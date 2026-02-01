
using Application.Proposta.DTO;
using Application.Proposta.Ports;
using Application.Proposta.Request;
using Application.Proposta.Response;
using Domain.Entities;
using Domain.Exceptons;
using Domain.Ports;

namespace Application
{
    public class PropostaManager : IPropostaManager
    {
        private IPropostaRepository _propostaRepository;
        public PropostaManager(IPropostaRepository propostaRepository) 
        {
            _propostaRepository = propostaRepository;
        }
        public async Task<PropostaResponse> CreateProposta(CreatePropostaRequest request)
        {
            try
            {
                var proposta = PropostaDTO.MapToEntity(request.Data);

                await proposta.Save(_propostaRepository);

                request.Data.Id = proposta.Id;                   

                return new PropostaResponse
                {
                    Data = request.Data,
                    Success = true
                };
            }
            catch (InvalidPersonDocumentIdException e) 
            {
                return new PropostaResponse
                {
                    ErrorCode = ErrorCode.INVALID_PERSON_ID,
                    Success = false,
                    Message = "O ID passado esta invalido"
                };
            }
            catch (MissingRequiredInformation e)
            {
                return new PropostaResponse
                {
                    ErrorCode = ErrorCode.MISSION_REQUIRED_INFORMATION,
                    Success = false,
                    Message = "Informações requeridas."
                };
            }
            catch (Exception)
            {
                return new PropostaResponse
                {
                    ErrorCode = ErrorCode.MISSION_REQUIRED_INFORMATION,
                    Success = false,
                    Message = "Informações requeridas."
                };
            }

        }

        public async Task<PropostaResponse> GetProposta(int propostaId)
        {
            var proposta = await _propostaRepository.GetProposta(propostaId);

            if (proposta == null)
            {
                return new PropostaResponse
                {
                    ErrorCode = ErrorCode.NOT_FOUND,
                    Success = false,
                    Message = "Cliente não encontrado."
                };
            }

            return new PropostaResponse
            {
                Data = PropostaDTO.MapFromEntity(proposta),
                Success = true
            };
        }

        public async Task<PropostaResponse> UpdateProposta(int id, PropostaDTO propostaDto)
        {
            var propostaExistente = await _propostaRepository.GetProposta(id);

            if (propostaExistente == null)
                return new PropostaResponse { Success = false, ErrorCode = ErrorCode.NOT_FOUND, Message = "Proposta não encontrada." };

            // Mapeia os dados do DTO para a entidade existente
            // Você pode criar um método MapToEntity que aceite o destino para manter a referência
            //propostaExistente.Status = propostaDto.Status;
            // ... outros campos ...

            await _propostaRepository.Update(propostaExistente);

            return new PropostaResponse { Data = propostaDto, Success = true };
        }
    }
}
