using Application.Proposta.DTO;
using Application.Proposta.Request;
using Application.Proposta.Response;

namespace Application.Proposta.Ports
{
    public interface IPropostaManager
    {
        Task<PropostaResponse> CreateProposta(CreatePropostaRequest request);
        Task<PropostaResponse> GetProposta(int clienteId);
        Task<PropostaResponse> UpdateProposta(int id, PropostaDTO propostaDto);
        Task<List<PropostaDTO>> ListPropostas();
    }
}
