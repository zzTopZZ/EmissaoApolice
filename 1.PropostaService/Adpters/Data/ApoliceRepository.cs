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
    public class ApoliceRepository : IApoliceRepository
    {
        private EmissaoDbContext _context;
        public ApoliceRepository(EmissaoDbContext context)
        {
            _context = context;
        }
        public async Task<int> Create(Apolice apolice)
        {
            _context.Apolices.Add(apolice);
            await _context.SaveChangesAsync();
            return apolice.Id;
        }

        public async Task<Apolice> GetApolice(int Id)
        {
            var apolices = await _context.Apolices.ToListAsync();
            return await _context.Apolices.Where(c => c.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Apolice>> ListAll()
        {
            return await _context.Apolices.AsNoTracking().ToListAsync();
        }
    }
}
