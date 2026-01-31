using Domain.Entities;

namespace Domain.Ports
{
    public interface IClienteRepository
    {
        Task<Cliente> GetCliente(int Id);

        Task<int> Create(Cliente cliente);
    }
}
