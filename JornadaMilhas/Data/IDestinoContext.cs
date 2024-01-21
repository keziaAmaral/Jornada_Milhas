using Jornada_Milhas.Models;
using Microsoft.EntityFrameworkCore;

namespace Jornada_Milhas.Data
{
    public interface IDestinoContext
    {
        public DbSet<Destino> Destino { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        public Destino Update(Destino depoimento);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
