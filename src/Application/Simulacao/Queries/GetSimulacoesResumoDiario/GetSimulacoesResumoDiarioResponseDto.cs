namespace SimulacaoDeCredito.Application.Queries.GetSimulacoesResumoDiario
{
    public class GetSimulacoesResumoDiarioResponseDto
    {
        public DateOnly Data { get; set; }
        public ICollection<SimulacaoResumoDiarioDto> Simulacoes { get; set; } = new List<SimulacaoResumoDiarioDto>();
    }

    public class SimulacaoResumoDiarioDto
    {
        public required int CodigoProduto { get; set; }
        public required string? DescricaoProduto { get; set; }
        public required decimal TaxaMediaJuro { get; set; }
        public required decimal ValorMedioPrestacao { get; set; }
        public required decimal ValorTotalDesejado { get; set; }
        public required decimal ValorTotalCredito { get; set; }
    }
}
