using Application;
using Application.Apolice.DTO;
using Application.Apolice.Ports;
using Application.Apolice.Response;
using Application.Cliente.DTO;
using Application.Cliente.Ports;
using Application.Cliente.Request;
using Application.Contratacao.Responses;
using Application.Proposta.DTO;
using Application.Proposta.Ports;
using Application.Proposta.Request;
using Application.Proposta.Response;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Domain.Entities.Proposta;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PropostaController : ControllerBase
    {
        private readonly ILogger<PropostaController> _logger;
        private readonly IPropostaManager _propostaManager;
        private readonly IClienteManager _clienteManager;
        public PropostaController(ILogger<PropostaController> logger, IPropostaManager propostaManager, IClienteManager clienteManager)
        {
            _logger = logger;
            _propostaManager = propostaManager;
            _clienteManager = clienteManager;

        }


        [HttpPost("post")]
        public async Task<ActionResult<PropostaDTO>> Post(PropostaDTO proposta)
        {
            var request = new CreatePropostaRequest
            {
                Data = proposta
            };

            var clienteRes = await _clienteManager.GetCliente(proposta.ClienteId);

            if (!clienteRes.Success)
            {
                return BadRequest("Cliente não encontrado para criar a proposta.");
            }

            var cliente = new Cliente() {
                Id = clienteRes.Data.Id,
                Nome = clienteRes.Data.Nome,
                Sobrenome = clienteRes.Data.Sobrenome,
                Email = clienteRes.Data.Email
            };

            proposta.NomeCliente = cliente.Nome + " " + cliente.Sobrenome;

            var res = await _propostaManager.CreateProposta(request);

            if (res.Success)
                return Created("" , res.Data);

            if (res.ErrorCode == ErrorCode.NOT_FOUND)
            {
                return NotFound(res);
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

        [HttpGet("get")]
        public async Task<ActionResult<PropostaDTO>> Get(int propostaId)
        {
            var res = await _propostaManager.GetProposta(propostaId);

            if (res.Success)
                return Created("", res.Data);

            return NotFound(res);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PropostaDTO>> Put(int id, PropostaDTO proposta)
        {
            // Opcional: Validar se o id da URL coincide com o do body
            proposta.Id = id;

            var res = await _propostaManager.UpdateProposta(id, proposta);

            if (res.Success)
                return Ok(res.Data);

            if (res.ErrorCode == ErrorCode.NOT_FOUND)
                return NotFound(res);

            return BadRequest(res);
        }

        [HttpGet("list")] 
        public async Task<ActionResult<List<PropostaDTO>>> List()
        {
            var lista = await _propostaManager.ListPropostas();

            if (lista == null || !lista.Any())
            {
                return NoContent(); // Retorna 204 se a lista estiver vazia
            }

            return Ok(lista);
        }
    }
}
