using Application.Apolice.DTO;
using Application.Apolice.Request;
using Application.Apolice.Response;
using Application.Booking.Dtos;
using Application.Contratacao.Responses;
using Application.Proposta.DTO;

namespace Application.Apolice.Ports
{
    public interface IApoliceManager
    {
        Task<ApoliceResponse> CreateApolice(CreateApoliceRequest request);
        Task<ApoliceResponse> GetApolice(int clienteId);
        Task<ContratacaoResponse> ContratacaoProposta(ContratacaoRequestDto contratacaoRequestDto);
        Task<IEnumerable<ApoliceDTO>> ListarApolices();
    }
}
