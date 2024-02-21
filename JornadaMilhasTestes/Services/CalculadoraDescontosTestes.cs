using Jornada_Milhas.Models;
using Jornada_Milhas.Services.Descontos;
using Xunit;

namespace JornadaMilhasTestes.Services
{
    public class CalculadoraDescontosTestes
    {
        [Fact]
        public void Calcular_Deve_Aplicar_DescontoAssociado_e_DescontoMilhas()
        {
            var valor = 1000M;
            var expected = valor * 0.95M * 0.9M;

            var result = new CalculadoraDescontos(valor)
             .Calcular(CalculoDescontos.DescontoMilhas)
             .Calcular(CalculoDescontos.DescontoAssociado)
             .Valor;

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Calcular_Deve_Aplicar_DescontoAssociado()
        {
            var valor = 1000M;
            var expected = valor * 0.9M;

            var result = new CalculadoraDescontos(valor)
             .Calcular(CalculoDescontos.DescontoAssociado)
             .Valor;

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Calcular_Deve_Aplicar_DescontoMilhas()
        {
            var valor = 1000M;
            var expected = valor * 0.95M;

            var result = new CalculadoraDescontos(valor)
             .Calcular(CalculoDescontos.DescontoMilhas)
             .Valor;

            Assert.Equal(expected, result);
        }

    }
}
