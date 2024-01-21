using Jornada_Milhas.Data;
using Jornada_Milhas.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Jornada_Milhas.Services
{
    public class DepoimentoService : IDepoimentoService
    {
        private readonly IDepoimentoContext _depoimentoContext;
        public DepoimentoService(IDepoimentoContext depoimentoContext)
        {
            _depoimentoContext = depoimentoContext;
        }

        public async Task AddAsync(Depoimento depoimento)
        {
            await _depoimentoContext.Depoimento.AddAsync(depoimento);
            await _depoimentoContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = _depoimentoContext.Depoimento.First(x => x.Id == id);
            item.Deleted = true;
            item.DeletedDate = DateTime.Now;

            await _depoimentoContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Depoimento>> GetListAsync(int skip, int take)
        {
            return await _depoimentoContext.Depoimento
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<IEnumerable<Depoimento>> GetRandomAsync()
        {
            var random = new Random();
            return await _depoimentoContext.Depoimento
                .Where(x => x.Deleted == false)
                .OrderBy(r => Guid.NewGuid())
                .Take(3)
                .ToListAsync();
        }

        public async Task UpdateAsync(Depoimento depoimento)
        {
            await Task.Run(() =>
            {
                _depoimentoContext.Depoimento.Update(new Depoimento
                { 
                    Comentario = depoimento.Comentario,
                    NomeUsuario = depoimento.NomeUsuario,
                    Foto = depoimento.Foto,
                    Deleted = depoimento.Deleted,
                    DeletedDate = depoimento.DeletedDate
                });

                _depoimentoContext.SaveChangesAsync();
            });
        }
    }
}
