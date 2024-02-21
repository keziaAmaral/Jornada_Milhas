using Jornada_Milhas.Models;
using Jornada_Milhas.Services.Pagamentos;
using Moq;
using Xunit;

public class PagamentoStrategyTestes
{
    [Fact]
    public async Task RealizarPagamento_ChamadaCorretaDaEstrategia()
    {
        var pagamentoMock = new Mock<IPagamentoStrategy>();
        pagamentoMock
            .Setup(x => x.RealizarPagamento(It.IsAny<Pagamento>()))
            .ReturnsAsync("Pagamento realizado com sucesso");

        var pagamentoStrategy = new PagamentoStrategy(pagamentoMock.Object);
        var pagamento = new Pagamento();

        var resultado = await pagamentoStrategy.RealizarPagamento(pagamento);

        Assert.Equal("Pagamento realizado com sucesso", resultado);
        pagamentoMock.Verify(x => x.RealizarPagamento(pagamento), Times.Once);
    }
}