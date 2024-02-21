using Jornada_Milhas.Data;
using Jornada_Milhas.Models;

namespace Jornada_Milhas.Services.Pagamentos
{
    public class PagamentoService : IPagamentoService
    {
        private readonly IPagamentoContext _context;

        public PagamentoService(IPagamentoContext pagamentoContext)
        {
            _context = pagamentoContext;
        }

        public async Task<string> EfetuarPagamento(Pagamento pagamento)
        {

            PagamentoStrategy pagamentoStrategy;

            if (pagamento.TipoPagamento.ToLower() == "cartao")
            {
                pagamentoStrategy = new PagamentoStrategy(new PagamentoCartao(_context));
            }

            else if (pagamento.TipoPagamento.ToLower() == "pix")
            {
                pagamentoStrategy = new PagamentoStrategy(new PagamentoPix(_context));
            }

            else { throw new Exception("Opcao de pagamento não informada"); }

            return await pagamentoStrategy.RealizarPagamento(pagamento);

        }
    }
}
