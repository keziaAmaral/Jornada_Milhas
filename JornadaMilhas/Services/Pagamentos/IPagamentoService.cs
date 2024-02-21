using Jornada_Milhas.Models;

namespace Jornada_Milhas.Services.Pagamentos
{
    public interface IPagamentoService
    {
        Task<string> EfetuarPagamento(Pagamento pagamento);
    }
}
