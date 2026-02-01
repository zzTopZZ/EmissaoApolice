using Application.Apolice.Request;
using Application.Apolice.Response;

namespace Application.Apolice.Ports
{
    public interface IApoliceManager
    {
        Task<ApoliceResponse> CreateApolice(CreateApoliceRequest request);
        Task<ApoliceResponse> GetApolice(int clienteId);
       
    }
}
