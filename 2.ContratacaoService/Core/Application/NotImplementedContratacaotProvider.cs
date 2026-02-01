using Application;
using Application.Contratacao.Responses;
using Application.Payment;

namespace Payments.Application
{
    public class NotImplementedContratacaotProvider : IContratacaoProcessor
    {
        public Task<ContratacaoResponse> CaptureContratacao(string contratacaoIntention)
        {
            var contratcaoResponse = new ContratacaoResponse() 
            { 
                Success = false,
                ErrorCode = ErrorCode.CONTRATACAO_PROVIDER_NOT_IMPLEMENTED,
                Message = "The selected payment provider is not available at the moment"
            };

            return Task.FromResult(contratcaoResponse);
        }
    }
}
