using Application.Cliente.DTO;
using Application.Cliente.Ports;
using Application.Cliente.Request;
using Application.Cliente.Response;
using Domain.Entities;
using Domain.Exceptons;
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

                await cliente.Save(_clienteRepository);

                request.Data.Id = cliente.Id;
                    
                // request.Data.Id = await _clienteRepository.Create(cliente);

                return new ClienteResponse
                {
                    Data = request.Data,
                    Success = true
                };
            }
            catch (InvalidPersonDocumentIdException e) 
            {
                return new ClienteResponse
                {
                    ErrorCode = ErrorCode.INVALID_PERSON_ID,
                    Success = false,
                    Message = "O ID passado esta invalido"
                };
            }
            catch (MissingRequiredInformation e)
            {
                return new ClienteResponse
                {
                    ErrorCode = ErrorCode.MISSION_REQUIRED_INFORMATION,
                    Success = false,
                    Message = "Informações requeridas."
                };
            }
            catch (Exception)
            {
                return new ClienteResponse
                {
                    ErrorCode = ErrorCode.MISSION_REQUIRED_INFORMATION,
                    Success = false,
                    Message = "Informações requeridas."
                };
            }

        }

        public async Task<ClienteResponse> GetCliente(int clienteId)
        {
            var cliente = await _clienteRepository.GetCliente(clienteId);

            if (cliente == null)
            {
                return new ClienteResponse
                {
                    ErrorCode = ErrorCode.NOT_FOUND,
                    Success = false,
                    Message = "Cliente não encontrado."
                };
            }

            return new ClienteResponse
            {
                Data = ClienteDTO.MapFromEntity(cliente),
                Success = true
            };
        }
    }
}
