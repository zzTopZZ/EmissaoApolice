using Domain.Entities;

namespace Domain.Ports
{
    public interface IApoliceRepository
    {
        Task<Apolice> GetApolice(int Id);

        Task<int> Create(Apolice apolice);
    }
}
