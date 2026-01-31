using Application.Cliente.DTO;
using Application.Cliente.Request;
using Application.Cliente.Response;
using System.Threading.Tasks;

namespace Application.Cliente.Ports
{
    public interface IClienteManager
    {
        Task<ClienteResponse> CreateCliente(CreateClienteRequest request);
        Task<ClienteResponse> GetCliente(int clienteId);
       
    }
}
