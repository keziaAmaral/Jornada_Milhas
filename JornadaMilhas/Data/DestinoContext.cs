using Jornada_Milhas.Models;
using Microsoft.EntityFrameworkCore;

namespace Jornada_Milhas.Data
{
    public class DestinoContext : DbContext, IDestinoContext
    {
        public DestinoContext(DbContextOptions<DestinoContext> options) : base(options)
        {

        }

        public DbSet<Destino> Destino { get; set; }

        public Destino Update(Destino destino)
        {
            return Destino.Update(destino).Entity;
        }
    }


}
