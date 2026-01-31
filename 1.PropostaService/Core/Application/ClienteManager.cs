using Application.Cliente.DTO;
using Application.Cliente.Ports;
using Application.Cliente.Request;
using Application.Cliente.Response;
using Domain.Ports;

namespace Application
{
    public class ClienteManager : IClienteManager
    {
        private IClienteRepository _clienteRepository;
        public ClienteManager(IClienteRepository clienteRepository) 
        {
            _clienteRepository = clienteRepository;
        }
        public async Task<ClienteResponse> CreateCliente(CreateClienteRequest request)
        {
            try
            {
                var cliente = ClienteDTO.MapToEntity(request.Data);

                request.Data.Id = await _clienteRepository.Create(cliente);

                return new ClienteResponse
                {
                    Data = request.Data,
                    Success = true
                };
            }
            catch (Exception)
            {
                return new ClienteResponse
                {
                    ErrorCode = ErrorCode.COULD_NOT_STORE_DATA,
                    Success = false,
                    Message = "Erro ao criar cliente"
                };
            }

        }
    }
}
