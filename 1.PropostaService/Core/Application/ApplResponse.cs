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


        MANAUL_INVALID_CONTRATACAO_INTENTION = 500,
        CONTRATACAO_INVALID_PAYMENT_INTENTION = 501,
        CONTRATACAO_PROVIDER_NOT_IMPLEMENTED = 502
    }

    public abstract class ApplResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public ErrorCode ErrorCode { get; set; }

    }
}



