using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class EmissaoDbContext : DbContext
    {
        public EmissaoDbContext(DbContextOptions<EmissaoDbContext> options) : base(options)
        {
        }
        // DbSets for your entities go here

        public virtual DbSet<Cliente> Clientes { get; set; }

        public virtual DbSet<Proposta> Propostas { get; set; }

        public virtual DbSet<Apolice> Apolices { get; set; }

    }
}
