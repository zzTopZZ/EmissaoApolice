
using Application.Apolice.DTO;
using Application.Apolice.Ports;
using Application.Apolice.Request;
using Application.Apolice.Response;
using Application.Booking.Dtos;
using Application.Contratacao.Ports;
using Application.Contratacao.Responses;
using Application.Proposta.DTO;
using Domain.Entities;
using StatusApolice = Domain.Enums.Status;
using Domain.Exceptons;
using Domain.Ports;
using Application.Contratacao.Dtos;

namespace Application
{
    public class ApoliceManager : IApoliceManager
    {
        private IApoliceRepository _apoliceRepository;
        private IPropostaRepository _propostaRepository;
        private readonly IContratacaoProcessorFactory _contratacaoProcessorFactory;
        public ApoliceManager(IApoliceRepository apoliceRepository,
                             IContratacaoProcessorFactory contratacaoProcessorFactory,
                             IPropostaRepository propostaRepository) 
        {
            _apoliceRepository = apoliceRepository;
            _contratacaoProcessorFactory = contratacaoProcessorFactory;
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
            catch (InvalidPersonDocumentIdException) 
            {
                return new ApoliceResponse
                {
                    ErrorCode = ErrorCode.INVALID_PERSON_ID,
                    Success = false,
                    Message = "O ID passado esta invalido"
                };
            }
            catch (MissingRequiredInformation )
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
                    Message = "Proposta não encontrada."
                };
            }

            if (proposta.Status == (int)StatusApolice.Emitida)
            {
                return new ContratacaoResponse
                {
                    ErrorCode = ErrorCode.PROPOSTA_JA_EMITIDA,
                    Success = false,
                    Message = "Proposta já emitida."
                };
            }
            else if (proposta.Status == (int)StatusApolice.Analise)
            {
                return new ContratacaoResponse
                {
                    ErrorCode = ErrorCode.PROPOSTA_EM_ANALISE,
                    Success = false,
                    Message = "Proposta em analise."
                };
            }
            else if (proposta.Status == (int)StatusApolice.Rejeitada)
            {
                return new ContratacaoResponse
                {
                    ErrorCode = ErrorCode.PROPOSTA_REJEITADA,
                    Success = false,
                    Message = "Proposta em analise."
                };
            }
            else if (proposta.Status == (int)StatusApolice.Criada)
            {
                return new ContratacaoResponse
                {
                    ErrorCode = ErrorCode.PROPOSTA_EM_ANDAMENTO,
                    Success = false,
                    Message = "Proposta em andamento."
                };
            }
            else if (proposta.Status == (int)StatusApolice.Aprovada)
            {
                var apolice = new ApoliceDTO();

                apolice.PropostaId = proposta.Id;
                apolice.DataContratacao = DateTime.UtcNow;
                apolice.ValorSegurado = proposta.ValorProposta;
                apolice.ValorPremio = proposta.ValorPremio;
                var apoliceEntity = ApoliceDTO.MapToEntity(apolice);
                var apoliceId = await _apoliceRepository.Create(apoliceEntity);

                proposta.Status = (int)StatusApolice.Emitida;
                await _propostaRepository.Update(proposta);

                var dto = new ContratacaoStateDto
                {
                    DataCriacao = DateTime.UtcNow,
                    Message = $"Successfully propostaId {apoliceId}",
                    ContratacaoId = apoliceId.ToString(),
                    Status = Status.Sucesso
                };

                return new ContratacaoResponse
                {
                    Success = true,
                    Data = dto,
                    Message = "Contratacao processada com sucesso"
                };

            }

            return response;
        }

        public async Task<IEnumerable<ApoliceDTO>> ListarApolices()
        {
            var apolices = await _apoliceRepository.ListAll(); 

            return apolices.Select(a => ApoliceDTO.MapFromEntity(a)).ToList();
        }
    }
}
