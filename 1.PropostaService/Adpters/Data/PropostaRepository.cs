using Domain.Entities;
using Domain.Ports;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class PropostaRepository : IPropostaRepository
    {
        private EmissaoDbContext _context;
        public PropostaRepository(EmissaoDbContext context)
        {
            _context = context;
        }
        public async Task<int> Create(Proposta proposta)
        {
            _context.Propostas.Add(proposta);
            await _context.SaveChangesAsync();
            return proposta.Id;
        }

        public async Task<Proposta> GetProposta(int Id)
        {
            var propostas = await _context.Propostas.ToListAsync();
            return await _context.Propostas.Where(c => c.Id == Id).FirstOrDefaultAsync();
        }
    }
}
