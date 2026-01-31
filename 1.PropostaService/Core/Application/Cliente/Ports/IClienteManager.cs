using Application.Cliente.Request;
using Application.Cliente.Response;

namespace Application.Cliente.Ports
{
    public interface IClienteManager
    {
        Task<ClienteResponse> CreateCliente(CreateClienteRequest request);
    }
}
