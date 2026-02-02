using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public enum ErrorCode
    {
        NOT_FOUND = 1,
        COULD_NOT_STORE_DATA = 2,
        INVALID_PERSON_ID = 3,
        MISSION_REQUIRED_INFORMATION = 4,
        INVALID_TO_EMAIL = 5,

        PROPOSTA_JA_EMITIDA = 300,
        PROPOSTA_EM_ANALISE = 301,
        PROPOSTA_REJEITADA = 301,
        PROPOSTA_EM_ANDAMENTO = 302,
        PROPOSTA_NAO_PODE_SER_ALTERADA = 303,

        MANAUL_INVALID_CONTRATACAO_INTENTION = 500,
        CONTRATACAO_INVALID_CONTRATACAO_INTENTION = 501,
        CONTRATACAO_PROVIDER_NOT_IMPLEMENTED = 502
    }

    public abstract class ApplResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public ErrorCode ErrorCode { get; set; }

    }
}



