using Jornada_Milhas.Models;

namespace Jornada_Milhas.Services
{
    public interface IDestinosService
    {
        Task<IEnumerable<Destino>> GetListAsync(int skip, int take);
        Task AddAsync(Destino destino);
        Task DeleteAsync(int id);
        Task UpdateAsync(Destino destino);
    }
}
