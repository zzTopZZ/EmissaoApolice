using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports
{
    public interface IPropostaRepository
    {
        Task<Proposta> Get(int Id);
        Task<List<Proposta>> GetAll();
        Task<Proposta> Save(Proposta proposta);
    }
}
