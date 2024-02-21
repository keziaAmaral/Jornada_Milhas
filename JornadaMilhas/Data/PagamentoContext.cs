using Jornada_Milhas.Models;
using Microsoft.EntityFrameworkCore;

namespace Jornada_Milhas.Data
{
    public class PagamentoContext : DbContext, IPagamentoContext
    {
        public PagamentoContext(DbContextOptions<PagamentoContext> options) : base(options)
        {

        }

        public DbSet<Pagamento> Pagamento { get; set; }
    }
}
