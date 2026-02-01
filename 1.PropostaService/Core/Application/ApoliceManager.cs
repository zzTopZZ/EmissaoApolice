
using Application.Apolice.DTO;
using Application.Apolice.Ports;
using Application.Apolice.Request;
using Application.Apolice.Response;
using Application.Booking.Dtos;
using Application.Contratacao.Ports;
using Application.Contratacao.Responses;
using Domain.Entities;
using Domain.Exceptons;
using Domain.Ports;

namespace Application
{
    public class ApoliceManager : IApoliceManager
    {
        private IApoliceRepository _apoliceRepository;
        private IPropostaRepository _propostaRepository;
        private readonly IContratacaoProcessorFactory _contratacaoProcessorFactory;
        public ApoliceManager(IApoliceRepository apoliceRepository,
                             IContratacaoProcessorFactory paymentProcessorFactory,
                             IPropostaRepository propostaRepository) 
        {
            _apoliceRepository = apoliceRepository;
            _contratacaoProcessorFactory = paymentProcessorFactory;
            _propostaRepository = propostaRepository;
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

        public async Task<ContratacaoResponse> ContratacaoProposta(ContratacaoRequestDto contratacaoRequestDto)
        {
            var contratacaoProcessor = _contratacaoProcessorFactory.GetContratacaoProcessor(contratacaoRequestDto.SelectedContratacaoProvider);

            var response = await contratacaoProcessor.CaptureContratacao(contratacaoRequestDto.ContratacaoIntention);

            var proposta = await _propostaRepository.GetProposta(contratacaoRequestDto.PropostaId);

            if (proposta == null)
            {
                return new ContratacaoResponse
                {
                    ErrorCode = ErrorCode.NOT_FOUND,
                    Success = false,
                    Message = "Cliente não encontrado."
                };
            }

            if (response.Success)
            {
                return new ContratacaoResponse
                {
                    Success = true,
                    Data = response.Data,
                    Message = "Payment successfully processed"
                };
            }

            return response;
        }
    }
}
