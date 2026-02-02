using Application;
using Application.Booking.Dtos;
using Application.Contratacao.Dtos;
using Application.Contratacao.Ports;
using Application.Contratacao.Responses;
using Application.Manual.Exceptions;
using Application.Payment;
using Domain.Ports;

namespace Contratacao.Application.Manual
{
    public class ContratacaoManualAdapter : IContratacaoProcessor
    {
        public Task<ContratacaoResponse> CaptureContratacao(string contratacaoIntention)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(contratacaoIntention))
                {
                    throw new InvalidContraracaoIntencionException();
                }

                contratacaoIntention += "/success";

                var dto = new ContratacaoStateDto
                {
                    DataCriacao = DateTime.UtcNow,
                    Message = $"Successfully paid {contratacaoIntention}",
                    ContratacaoId = "123",
                    Status = Status.Sucesso
                };

                var response = new ContratacaoResponse
                {
                    Data = dto,
                    Success = true,
                    Message = "Contratacao successfully processed"
                };

                return Task.FromResult(response);
            }
            catch (InvalidContraracaoIntencionException)
            {
                var resp = new ContratacaoResponse()
                {
                    Success = false,
                    ErrorCode = ErrorCode.CONTRATACAO_INVALID_CONTRATACAO_INTENTION,
                    Message = "The selected contratacao intention is invalid"
                };
                return Task.FromResult(resp);
            }
        }

        //public Task<ContratacaoResponse> ContratacaoAutomatica(string contratacaoInteracao)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ContratacaoResponse> ContratacaoManual(string contratacaoInteracao)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(contratacaoInteracao))
        //        {
        //            throw new InvalidContraracaoManualIntencionException();
        //        }

        //        contratacaoInteracao = "/Success";

        //        var dto = new ContratacaoStateDto
        //        {
        //            Status = Status.Sucesso,
        //            ContratacaoId = "12345",
        //            DataCriacao = DateTime.UtcNow,
        //            Message = "Contratação manual realizada com sucesso."
        //        };

        //        var response = new ContratacaoResponse()
        //        {
        //            Success = true,
        //            Data = dto
        //        };

        //        return Task.FromResult(response);
        //    }
        //    catch (InvalidContraracaoManualIntencionException)
        //    {
        //        var reso = new ContratacaoResponse()
        //        {
        //            Success = false,
        //            ErrorCode = ErrorCode.MANAUL_INVALID_CONTRATACAO_INTENTION,

        //        };
        //        return Task.FromResult(reso);
        //    }
        //}
    }
}
