using System;
using System.Collections.Generic;

namespace SimulacaoDeCredito.src.Infra.BaseSimulacao.Models;

public partial class SimulacaoParcelaModel
{
    public int? IdSimulacaoParcela { get; set; }

    public int? IdSimulacaoTabela { get; set; }

    public int Numero { get; set; }

    public decimal ValorAmortizacao { get; set; }

    public decimal ValorJuros { get; set; }

    public decimal ValorPrestacao { get; set; }

    public virtual SimulacaoTabelaModel IdSimulacaoTabelaNavigation { get; set; } = null!;
}
