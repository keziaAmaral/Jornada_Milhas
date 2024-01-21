using Jornada_Milhas.Models;

namespace Jornada_Milhas.Services
{
    public interface IDepoimentoService
    {
        Task AddAsync(Depoimento depoimento);
        Task<IEnumerable<Depoimento>> GetListAsync(int skip, int take);
        Task<IEnumerable<Depoimento>> GetRandomAsync();
        Task DeleteAsync(int id);
        Task UpdateAsync(Depoimento depoimento);
    }
}
