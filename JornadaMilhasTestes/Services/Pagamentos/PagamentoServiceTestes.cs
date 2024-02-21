using Jornada_Milhas.Data;
using Jornada_Milhas.Models;
using Jornada_Milhas.Services.Pagamentos;
using JornadaMIlhasTestes.Helper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace JornadaMilhasTestes.Services.Pagamentos
{
    public class PagamentoServiceTestes
    {
        private readonly PagamentoService _subject;
        private readonly Mock<IPagamentoContext> _context;
        private readonly Mock<DbSet<Pagamento>> _mockDbSet;
        public PagamentoServiceTestes()
        {
            _context = new Mock<IPagamentoContext>();
            _subject = new PagamentoService(_context.Object);
            _mockDbSet = new Mock<DbSet<Pagamento>>();
        }
        
        [Fact]
        public async Task EfetuarPagamento_Pix_DeveRealizarPagamento()
        {
            var pagamento = new Pagamento 
            { 
                Id = 1, 
                Valor = 500M, 
                TipoPagamento = "Pix"
            };

            var pagamentoList = new List<Pagamento> { pagamento }.AsQueryable();

            SetupDbSet(_mockDbSet, pagamentoList);

            _context
                .SetupGet(x => x.Pagamento)
                .Returns(_mockDbSet.Object);

            var expected = "Pagamento com Pix Realizado";

            var result = await _subject.EfetuarPagamento(pagamento);

            Assert.Equal(pagamento, _context.Object.Pagamento.FirstOrDefault());
            Assert.Equal(expected, result);
        }    
        
        [Fact]
        public async Task EfetuarPagamento_Cartao_DeveRealizarPagamento()
        {
            var pagamento = new Pagamento 
            { 
                Id = 1, 
                Valor = 500M, 
                TipoPagamento = "Cartao"
            };

            var pagamentoList = new List<Pagamento> { pagamento }.AsQueryable();

            SetupDbSet(_mockDbSet, pagamentoList);

            _context
                .SetupGet(x => x.Pagamento)
                .Returns(_mockDbSet.Object);

            var expected = "Pagamento com Cartao Realizado";

            var result = await _subject.EfetuarPagamento(pagamento);

            Assert.Equal(pagamento, _context.Object.Pagamento.FirstOrDefault());
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task EfetuarPagamento_OpcaoInvalida_DeveLancarExcecao()
        {
            var pagamento = new Pagamento
            {
                Id = 1,
                Valor = 500M,
                TipoPagamento = "Boleto"
            };

            var exception = await Assert.ThrowsAsync<Exception>(() => _subject.EfetuarPagamento(pagamento));
            Assert.Equal("Opcao de pagamento não informada", exception.Message);
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
