using Application.Contratacao.Responses;

namespace Application.Payment
{
    public interface IContratacaoProcessor
    {
        Task<ContratacaoResponse> CaptureContratacao(string contratacaoIntention);
    }
}
