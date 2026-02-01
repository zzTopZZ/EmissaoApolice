
using Application.Apolice.DTO;
using Application.Apolice.Ports;
using Application.Apolice.Request;
using Application.Apolice.Response;
using Domain.Entities;
using Domain.Exceptons;
using Domain.Ports;

namespace Application
{
    public class ApoliceManager : IApoliceManager
    {
        private IApoliceRepository _apoliceRepository;
        public ApoliceManager(IApoliceRepository apoliceRepository) 
        {
            _apoliceRepository = apoliceRepository;
        }
        public async Task<ApoliceResponse> CreateApolice(CreateApoliceRequest request)
        {
            try
            {
                var apolice = ApoliceDTO.MapToEntity(request.Data);

                await apolice.Save(_apoliceRepository);

                request.Data.Id = apolice.Id;

                return new ApoliceResponse
                {
                    Data = request.Data,
                    Success = true
                };
            }
            catch (InvalidPersonDocumentIdException e) 
            {
                return new ApoliceResponse
                {
                    ErrorCode = ErrorCode.INVALID_PERSON_ID,
                    Success = false,
                    Message = "O ID passado esta invalido"
                };
            }
            catch (MissingRequiredInformation e)
            {
                return new ApoliceResponse
                {
                    ErrorCode = ErrorCode.MISSION_REQUIRED_INFORMATION,
                    Success = false,
                    Message = "Informações requeridas."
                };
            }
            catch (Exception)
            {
                return new ApoliceResponse
                {
                    ErrorCode = ErrorCode.MISSION_REQUIRED_INFORMATION,
                    Success = false,
                    Message = "Informações requeridas."
                };
            }

        }
        public async Task<ApoliceResponse> GetApolice(int apoliceId)
        {
            var apolice = await _apoliceRepository.GetApolice(apoliceId);

            if (apolice == null)
            {
                return new ApoliceResponse
                {
                    ErrorCode = ErrorCode.NOT_FOUND,
                    Success = false,
                    Message = "Cliente não encontrado."
                };
            }

            return new ApoliceResponse
            {
                Data = ApoliceDTO.MapFromEntity(apolice),
                Success = true
            };
        }
    }
}
