using Jornada_Milhas.Models;
using Microsoft.EntityFrameworkCore;

namespace Jornada_Milhas.Data
{
    public interface IDepoimentoContext
    {
        public DbSet<Depoimento> Depoimento { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        public Depoimento Update(Depoimento depoimento);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
