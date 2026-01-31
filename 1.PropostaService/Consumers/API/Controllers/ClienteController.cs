using Application;
using Application.Cliente.DTO;
using Application.Cliente.Ports;
using Application.Cliente.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly IClienteManager _clienteManager;
        public ClienteController(ILogger<ClienteController> logger, IClienteManager clienteManager)
        {
            _logger = logger;
            _clienteManager = clienteManager;
        }

        [HttpPost]
        public async Task<ActionResult<ClienteDTO>> Post(ClienteDTO cliente)
        {
            var request = new CreateClienteRequest
            {
                Data = cliente
            };

            var res = await _clienteManager.CreateCliente(request);

            if (res.Success)
                return Created("" , res.Data);

            if (res.ErrorCode == ErrorCode.NOT_FOUND)
            {
                return BadRequest(res);
            }
            else if (res.ErrorCode == ErrorCode.COULD_NOT_STORE_DATA)
            {
                return BadRequest(res);
            }
            else if (res.ErrorCode == ErrorCode.INVALID_PERSON_ID)
            {
                return BadRequest(res);
            }
            else if (res.ErrorCode == ErrorCode.MISSION_REQUIRED_INFORMATION)
            {
                return BadRequest(res);
            }
            else if (res.ErrorCode == ErrorCode.INVALID_TO_EMAIL)
            {
                return BadRequest(res);
            }

            _logger.LogError("Erro ao criar cliente: {message}", res.Message);
            return BadRequest(500);
        }
    }
}
