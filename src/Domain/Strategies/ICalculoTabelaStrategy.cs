namespace SimulacaoDeCredito.Domain.Strategies;

public interface ICalculoTabelaStrategy
{
    SimulacaoTabela Calcular(decimal valor, int prazo, decimal taxa);
}