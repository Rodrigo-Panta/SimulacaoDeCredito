namespace SimulacaoDeCredito.Domain.Entities;

public class Simulacao
{
    public int? IdSimulacao { get; set; }
    public required int CodigoProduto { get; set; }
    public required string DescricaoProduto { get; set; }
    public required decimal TaxaJuros { get; set; }
    public required decimal ValorDesejado { get; set; }
    public required int Prazo { get; set; }
    public required ICollection<SimulacaoTabela> ResultadoSimulacao { get; set; }
    public required DateTime DataCriacao { get; set; }

}

