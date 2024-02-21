using Jornada_Milhas.Controllers;
using Jornada_Milhas.Models;
using Jornada_Milhas.Services.Pagamentos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace JornadaMilhasTestes.Controllers
{
    public class PagamentoControllerTeste
    {
        private readonly PagamentoController _subject;
        private readonly Mock<IPagamentoService> _pagamentoService;

        private Pagamento _pagamento = new Pagamento
        {
            Id = 1,
            Valor = 5000M,
            TipoPagamento = "Cartao"
        };

        public PagamentoControllerTeste()
        {
            _pagamentoService = new Mock<IPagamentoService>();
            _subject = new PagamentoController(_pagamentoService.Object);
        }

        [Fact]
        public async Task Should_EfetuarPagamento_Salvar_Pagamento()
        {
            var result = await _subject.EfetuarPagamento(_pagamento) as OkResult; ;

            Assert.Equal(StatusCodes.Status200OK, result?.StatusCode);
        }
    }
}
