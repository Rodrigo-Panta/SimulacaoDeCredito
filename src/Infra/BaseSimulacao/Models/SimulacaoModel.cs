using System;
using System.Collections.Generic;

namespace SimulacaoDeCredito.src.Infra.BaseSimulacao.Models;

public partial class SimulacaoModel
{
    public int? IdSimulacao { get; set; }

    public int CodigoProduto { get; set; }

    public string DescricaoProduto { get; set; } = null!;

    public decimal TaxaJuros { get; set; }

    public decimal ValorDesejado { get; set; }

    public int Prazo { get; set; }

    public DateTime DataCriacao { get; set; }

    public virtual ICollection<SimulacaoTabelaModel> SimulacaoTabelas { get; set; } = new List<SimulacaoTabelaModel>();
}
