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
        COULD_NOT_STORE_DATA = 2

    }

    public abstract class ApplResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public ErrorCode ErrorCode { get; set; }

    }
}



