using Jornada_Milhas.Data;
using Jornada_Milhas.Models;
using Microsoft.EntityFrameworkCore;

namespace Jornada_Milhas.Services
{
    public class DestinoService : IDestinosService
    {
        private readonly IDestinoContext _destinoContext;
        public DestinoService(IDestinoContext destinoContext)
        {
            _destinoContext = destinoContext;
        }

        public async Task AddAsync(Destino destino)
        {
            await _destinoContext.Destino.AddAsync(destino);
            await _destinoContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = _destinoContext.Destino.First(x => x.Id == id);
            item.Deleted = true;
            item.DeletedDate = DateTime.Now;

            await _destinoContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Destino>> GetByNameAsync(string name)
        {
           return await _destinoContext.Destino
                .Where(x => x.Nome.Contains(name))
                .ToListAsync();
        }

        public async Task<IEnumerable<Destino>> GetListAsync(int skip, int take)
        {
            return await _destinoContext.Destino
            .Skip(skip)
            .Take(take)
            .Where(x => !x.Deleted)
            .ToListAsync();
        }

        public async Task UpdateAsync(Destino destino)
        {
            await Task.Run(() =>
            {
                _destinoContext.Destino.Update(new Destino
                {
                    Nome = destino.Nome,
                    Preco = destino.Preco,
                    Deleted = destino.Deleted,
                    DeletedDate = destino.DeletedDate
                });

                _destinoContext.SaveChangesAsync();
            });
        }
    }
}
