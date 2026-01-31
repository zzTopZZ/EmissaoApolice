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
    public class ClienteRepository : IClienteRepository
    {
        private EmissaoDbContext _context;
        public ClienteRepository(EmissaoDbContext context)
        {
            _context = context;
        }
        public async Task<int> Create(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente.Id;
        }

        public async Task<Cliente> GetCliente(int Id)
        {
            var clientes = await _context.Clientes.ToListAsync();
            return await _context.Clientes.Where(c => c.Id == Id).FirstOrDefaultAsync();
        }
    }
}
