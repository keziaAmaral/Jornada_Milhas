using Jornada_Milhas.Models;
using Microsoft.EntityFrameworkCore;

namespace Jornada_Milhas.Data
{
    public class DepoimentoContext : DbContext, IDepoimentoContext
    {
        public DepoimentoContext(DbContextOptions<DepoimentoContext> options) : base (options) 
        {
            
        }

        public DbSet<Depoimento> Depoimento { get; set; }

        public Depoimento Update(Depoimento depoimento)
        {
            return Depoimento.Update(depoimento).Entity;
        }
    }
}
