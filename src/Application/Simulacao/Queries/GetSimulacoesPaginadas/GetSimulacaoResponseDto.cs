namespace SimulacaoDeCredito.Application.Queries.GetSimulacoesPaginadas
{
    public class GetSimulacaoResponseDto
    {
        public required int IdSimulacao { get; set; }
        public required decimal ValorDesejado { get; set; }
        public required int Prazo { get; set; }
        public required ICollection<ValorTotalParcelasResponseDto> ValorTotalParcelas { get; set; }
    }

    public class ValorTotalParcelasResponseDto
    {
        public required string Tipo { get; set; }
        public required decimal ValorTotalParcelas { get; set; }
    }
}
