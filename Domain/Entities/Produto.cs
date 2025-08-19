namespace SimulacaoDeCredito.Domain.Entities
{
    public class Produto
    {
        public required int CoProduto { get; set; }
        public required string NoProduto { get; set; }

        public required decimal PcTaxaDeJuros { get; set; }

        public required int NuMinimoMeses { get; set; }
        public required int NuMaximoMeses { get; set; }
        public required decimal VrMinimo { get; set; }
        public required decimal VrMaximo { get; set; }
    }
}
