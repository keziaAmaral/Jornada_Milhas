using Jornada_Milhas.Data;
using Jornada_Milhas.Models;
using Jornada_Milhas.Services.Descontos;

namespace Jornada_Milhas.Services.Pagamentos
{
    public class PagamentoCartao : IPagamentoStrategy
    {
        private readonly IPagamentoContext _pagamentoContext;
        public PagamentoCartao(IPagamentoContext pagamentoContext)
        {
            _pagamentoContext = pagamentoContext;
        }

        public async Task<string> RealizarPagamento(Pagamento pagamento)
        {
            pagamento.Valor = new CalculadoraDescontos(pagamento.Valor)
            .Calcular(CalculoDescontos.DescontoMilhas)
            .Calcular(CalculoDescontos.DescontoAssociado)
            .Valor;

            await _pagamentoContext.Pagamento.AddAsync(pagamento);
            await _pagamentoContext.SaveChangesAsync();

            return "Pagamento com Cartao Realizado";
        }
    }
}
