namespace SimulacaoDeCredito.Application.DTOs.Response
{
    public class CreateSimulacaoResponseDto
    {
        public required int idSimulacao { get; set; }
        public required int codigoProduto { get; set; }
        public required string descricaoProduto { get; set; }
        public required decimal taxaJuros { get; set; }
        public required ICollection<SimulacaoTabelaResponseDto> resultadoSimulacao { get; set; }

    }

    public class SimulacaoTabelaResponseDto
    {
        public required string tipo { get; set; }
        public required ICollection<SimulacaoParcelaResponseDto> parcelas { get; set; }
    }

    public class SimulacaoParcelaResponseDto
    {
        public required int numero { get; set; }
        public required decimal valorAmortizacao { get; set; }
        public required decimal valorJuros { get; set; }
        public required decimal valorPrestacao { get; set; }
    }
}
