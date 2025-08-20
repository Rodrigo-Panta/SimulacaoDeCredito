using System;
using System.Collections.Generic;

namespace SimulacaoDeCredito.src.Infra.BaseSimulacao.Models;

public partial class SimulacaoTabelaModel
{
    public int? IdSimulacaoTabela { get; set; }

    public int? IdSimulacao { get; set; }

    public string Tipo { get; set; } = null!;

    public virtual SimulacaoModel IdSimulacaoNavigation { get; set; } = null!;

    public virtual ICollection<SimulacaoParcelaModel> SimulacaoParcelas { get; set; } = new List<SimulacaoParcelaModel>();
}
