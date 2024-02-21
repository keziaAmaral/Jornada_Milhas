using Jornada_Milhas.Models;
using Microsoft.EntityFrameworkCore;

namespace Jornada_Milhas.Data
{
    public interface IPagamentoContext
    {
        public DbSet<Pagamento> Pagamento { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
