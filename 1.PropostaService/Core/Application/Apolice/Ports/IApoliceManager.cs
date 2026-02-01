using Application.Apolice.Request;
using Application.Apolice.Response;
using Application.Booking.Dtos;
using Application.Contratacao.Responses;

namespace Application.Apolice.Ports
{
    public interface IApoliceManager
    {
        Task<ApoliceResponse> CreateApolice(CreateApoliceRequest request);
        Task<ApoliceResponse> GetApolice(int clienteId);
        Task<ContratacaoResponse> ContratacaoProposta(ContratacaoRequestDto contratacaoRequestDto);
    }
}
