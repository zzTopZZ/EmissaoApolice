using Application.Contratacao.Dtos;
using Application.Contratacao.Responses;

namespace Application.Contratacao
{
    public interface IManualContatacaoService
    {
        Task<ContratacaoResponse> ContratacaoManual(string contratacaoInteracao);
        Task <ContratacaoResponse> ContratacaoAutomatica(string contratacaoInteracao);
    }
}
