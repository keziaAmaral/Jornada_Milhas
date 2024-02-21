namespace Jornada_Milhas.Services.Descontos
{
    public class CalculoDescontos
    {
        public static decimal DescontoAssociado(decimal valorTotal)
        => valorTotal * 0.9M;

        public static decimal DescontoMilhas(decimal valorTotal)
            => valorTotal * 0.95M;
    }

    public class CalculadoraDescontos
    {
        public decimal Valor { get; private set; }

        public CalculadoraDescontos(decimal valor)
        {
            Valor = valor;
        }

        public CalculadoraDescontos Calcular(Func<decimal, decimal> calculo)
        {
            Valor = calculo(Valor);
            return this;
        }
    }
}
