using Application.Contratacao.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contratacao.Responses
{
    public class ContratacaoResponse : ApplResponse
    {
        public ContratacaoStateDto Data { get; set; }
    }
}
