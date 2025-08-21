using Microsoft.EntityFrameworkCore;
using SimulacaoDeCredito.src.Infra.BaseSimulacao.Models;

namespace SimulacaoDeCredito.src.Infra.BaseSimulacao.Persistence;

public partial class BaseSimulacaoDbContext : DbContext
{
    public BaseSimulacaoDbContext()
    {
    }

    public BaseSimulacaoDbContext(DbContextOptions<BaseSimulacaoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<SimulacaoModel> Simulacaos { get; set; }

    public virtual DbSet<SimulacaoParcelaModel> SimulacaoParcelas { get; set; }

    public virtual DbSet<SimulacaoTabelaModel> SimulacaoTabelas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SimulacaoModel>(entity =>
        {
            entity.HasKey(e => e.IdSimulacao);

            entity.ToTable("Simulacao");

            entity.Property(e => e.IdSimulacao).HasColumnName("idSimulacao");
            entity.Property(e => e.CodigoProduto).HasColumnName("codigoProduto");
            entity.Property(e => e.DataCriacao).HasColumnName("dataCriacao");
            entity.Property(e => e.DescricaoProduto)
                .HasMaxLength(255)
                .HasColumnName("descricaoProduto");
            entity.Property(e => e.Prazo).HasColumnName("prazo");
            entity.Property(e => e.TaxaJuros)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("taxaJuros");
            entity.Property(e => e.ValorDesejado)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("valorDesejado");
        });

        modelBuilder.Entity<SimulacaoParcelaModel>(entity =>
        {
            entity.HasKey(e => e.IdSimulacaoParcela);

            entity.ToTable("SimulacaoParcela");

            entity.HasIndex(e => e.IdSimulacaoTabela, "IX_SimulacaoParcela_idSimulacaoTabela");

            entity.HasIndex(e => new { e.IdSimulacaoTabela, e.Numero }, "UQ_SimulacaoParcela_Tabela_Numero").IsUnique();

            entity.Property(e => e.IdSimulacaoParcela).HasColumnName("idSimulacaoParcela");
            entity.Property(e => e.IdSimulacaoTabela).HasColumnName("idSimulacaoTabela");
            entity.Property(e => e.Numero).HasColumnName("numero");
            entity.Property(e => e.ValorAmortizacao)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("valorAmortizacao");
            entity.Property(e => e.ValorJuros)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("valorJuros");
            entity.Property(e => e.ValorPrestacao)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("valorPrestacao");

            entity.HasOne(d => d.IdSimulacaoTabelaNavigation).WithMany(p => p.SimulacaoParcelas)
                .HasForeignKey(d => d.IdSimulacaoTabela)
                .HasConstraintName("FK_SimulacaoParcela_SimulacaoTabela");
        });

        modelBuilder.Entity<SimulacaoTabelaModel>(entity =>
        {
            entity.HasKey(e => e.IdSimulacaoTabela);

            entity.ToTable("SimulacaoTabela");

            entity.HasIndex(e => e.IdSimulacao, "IX_SimulacaoTabela_idSimulacao");

            entity.Property(e => e.IdSimulacaoTabela).HasColumnName("idSimulacaoTabela");
            entity.Property(e => e.IdSimulacao).HasColumnName("idSimulacao");
            entity.Property(e => e.ValorTotalParcelas).HasColumnName("valorTotalParcelas")
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .HasColumnName("tipo");

            entity.HasOne(d => d.IdSimulacaoNavigation).WithMany(p => p.SimulacaoTabelas)
                .HasForeignKey(d => d.IdSimulacao)
                .HasConstraintName("FK_SimulacaoTabela_Simulacao");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
