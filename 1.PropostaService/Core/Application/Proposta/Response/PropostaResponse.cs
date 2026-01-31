using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Proposta.Response
{
    public class PropostaResponse
    {
        public enum ErrorCodes
        {
            PropostaNotFound = 1,
            CouldNotStoreData = 2 //não foi possível armazenar dados
        }

        public abstract class Response
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            public ErrorCodes? ErrorCode { get; set; }
        }
    }
}
