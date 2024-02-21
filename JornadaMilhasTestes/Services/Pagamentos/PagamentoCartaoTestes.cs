using Jornada_Milhas.Data;
using Jornada_Milhas.Models;
using Jornada_Milhas.Services.Pagamentos;
using JornadaMIlhasTestes.Helper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace JornadaMilhasTestes.Services.Pagamentos
{
    public class PagamentoCartaoTestes
    {
        private readonly PagamentoCartao _subject;
        private readonly Mock<IPagamentoContext> _context;
        private readonly Mock<DbSet<Pagamento>> _mockDbSet;
        public PagamentoCartaoTestes()
        {
            _context = new Mock<IPagamentoContext>();
            _subject = new PagamentoCartao(_context.Object);
            _mockDbSet = new Mock<DbSet<Pagamento>>();
        }

        [Fact]
        public async Task Should_Add_Pagamento()
        {
            var pagamento = new Pagamento
            {
                Id = 5,
                Valor = 1000M,
                TipoPagamento = "Cartao"
            };

            var pagamentoList = new List<Pagamento> { pagamento }.AsQueryable();

            SetupDbSet(_mockDbSet, pagamentoList);

            _context
                .SetupGet(x => x.Pagamento)
                .Returns(_mockDbSet.Object);

            var expected = "Pagamento com Cartao Realizado";

            var pagamentoPosDesconto = pagamento.Valor * 0.9M * 0.95M;

            pagamento.Valor = pagamentoPosDesconto;

            var result = await _subject.RealizarPagamento(pagamento);

            Assert.Equal(expected, result);
            Assert.Equal(pagamento, _context.Object.Pagamento.FirstOrDefault());
            _context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        private void SetupDbSet(Mock<DbSet<Pagamento>> pagamentoMock, IQueryable<Pagamento> expectedPagamento)
        {
            pagamentoMock
                .As<IAsyncEnumerable<Pagamento>>()
                .Setup(ca => ca.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestDbAsyncEnumerator<Pagamento>(expectedPagamento.GetEnumerator()));

            pagamentoMock
                .As<IQueryable<Pagamento>>()
                .Setup(ca => ca.Provider)
                .Returns(new TestDbAsyncQueryProvider<Pagamento>(expectedPagamento.Provider));

            pagamentoMock
                .As<IQueryable<Pagamento>>()
                .Setup(ca => ca.Expression)
                .Returns(expectedPagamento.Expression);

            pagamentoMock
                .As<IQueryable<Pagamento>>()
                .Setup(ca => ca.ElementType)
                .Returns(expectedPagamento.ElementType);

            pagamentoMock
                .As<IQueryable<Pagamento>>()
                .Setup(ca => ca.GetEnumerator())
                .Returns(expectedPagamento.GetEnumerator());

            _context
                .SetupGet(c => c.Pagamento)
                .Returns(pagamentoMock.Object);
        }
    }
}
