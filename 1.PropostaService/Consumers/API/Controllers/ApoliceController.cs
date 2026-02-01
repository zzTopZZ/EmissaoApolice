using Application;
using Application.Apolice.DTO;
using Application.Apolice.Ports;
using Application.Apolice.Request;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApoliceController : ControllerBase
    {
        private readonly ILogger<ApoliceController> _logger;
        private readonly IApoliceManager _apoliceManager;
        public ApoliceController(ILogger<ApoliceController> logger, IApoliceManager apoliceManager)
        {
            _logger = logger;
            _apoliceManager = apoliceManager;
        }

        [HttpPost]
        public async Task<ActionResult<ApoliceDTO>> Post(ApoliceDTO apolice)
        {
            var request = new CreateApoliceRequest
            {
                Data = apolice
            };

            var res = await _apoliceManager.CreateApolice(request);

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

        [HttpGet]
        public async Task<ActionResult<ApoliceDTO>> Get(int apoliceId)
        {
            var res = await _apoliceManager.GetApolice(apoliceId);

            if (res.Success)
                return Created("", res.Data);

            return NotFound(res);
        }
    }
}
