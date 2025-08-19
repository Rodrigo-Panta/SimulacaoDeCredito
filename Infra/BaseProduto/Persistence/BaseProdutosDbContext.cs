using Microsoft.EntityFrameworkCore;
using SimulacaoDeCredito.Domain.Entities;

namespace SimulacaoDeCredito.Infrastructure.BaseProduto.Persistence;

public partial class BaseProdutosDbContext : DbContext
{
    public BaseProdutosDbContext()
    {
    }

    public BaseProdutosDbContext(DbContextOptions<BaseProdutosDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Produto> Produtos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Produto>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("produto");

            entity.Property(e => e.CoProduto).HasColumnName("co_produto");
            entity.Property(e => e.NoProduto)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("no_produto");
            entity.Property(e => e.NuMaximoMeses).HasColumnName("nu_maximo_meses");
            entity.Property(e => e.NuMinimoMeses).HasColumnName("nu_minimo_meses");
            entity.Property(e => e.PcTaxaJuros)
                .HasColumnType("decimal(10, 9)")
                .HasColumnName("pc_taxa_juros");
            entity.Property(e => e.VrMaximo)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("vr_maximo");
            entity.Property(e => e.VrMinimo)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("vr_minimo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
