using Jornada_Milhas.Models;

namespace Jornada_Milhas.Services.Pagamentos
{
    public interface IPagamentoStrategy
    {
        Task<string> RealizarPagamento(Pagamento pagamento);
    }
}
