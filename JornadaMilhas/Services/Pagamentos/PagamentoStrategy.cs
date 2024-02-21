using Jornada_Milhas.Models;

namespace Jornada_Milhas.Services.Pagamentos
{
    public class PagamentoStrategy
    {
        private IPagamentoStrategy _strategy;
        public PagamentoStrategy(IPagamentoStrategy strategy)
        {
            _strategy = strategy;
        }
        public async Task<string> RealizarPagamento(Pagamento pagamento)
        {
            return await _strategy.RealizarPagamento(pagamento);
        }
    }
}
