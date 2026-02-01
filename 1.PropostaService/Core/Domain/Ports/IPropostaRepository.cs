using Domain.Entities;

namespace Domain.Ports
{
    public interface IPropostaRepository
    {
        Task<Proposta> GetProposta(int Id);

        Task<int> Create(Proposta proposta);
    }
}
