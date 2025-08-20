namespace SimulacaoDeCredito.Domain.Entities;

public class Simulacao
{
    public required int idSimulacao { get; set; }
    public required int codigoProduto { get; set; }
    public required string descricaoProduto { get; set; }
    public required decimal taxaJuros { get; set; }
    public required ICollection<SimulacaoTabela> resultadoSimulacao { get; set; }

}

