public class SimulacaoTabela
{
    public required string Tipo { get; set; }
    public required ICollection<SimulacaoParcela> Parcelas { get; set; }
}

