namespace SimulacaoDeCredito.Domain.Entities
{
    public class Produto
    {
        public required int CoProduto { get; set; }
        public required string NoProduto { get; set; }

        public required decimal PcTaxaJuros { get; set; }

        public required short NuMinimoMeses { get; set; }
        public short? NuMaximoMeses { get; set; }
        public required decimal VrMinimo { get; set; }
        public decimal? VrMaximo { get; set; }
    }
}
