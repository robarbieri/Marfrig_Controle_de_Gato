using LSC.Infra.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSC.Infra.Data
{
    public class LSCContext : DbContext, IDisposable
    {

        #region Construtores

        public LSCContext() : this("LSCDb") { }

        public LSCContext(string connectionStringParameterName) : base($"name={connectionStringParameterName}")
        {
            Configuration.LazyLoadingEnabled = true;
        }

        #endregion

        #region Overrides

        protected override void OnModelCreating(DbModelBuilder m)
        {

            // Convencoes e Configuracoes
            // ------------------------------------------------------------------------------------------------
            m.Conventions.Remove<PluralizingTableNameConvention>();
            m.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            m.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            // Tabela Animal
            // ------------------------------------------------------------------------------------------------
            m.Entity<Animal>().ToTable("Animal");

            m.Entity<Animal>().Property(p => p.AnimalId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            m.Entity<Animal>().Property(p => p.Descricao).IsUnicode(false).IsRequired().HasMaxLength(80);
            m.Entity<Animal>().Property(p => p.Preco).IsRequired();

            m.Entity<Animal>().HasKey(e => e.AnimalId);
            m.Entity<Animal>().HasIndex(e => e.Descricao).IsUnique().HasName("AK_Animal_Descricao");

            // Tabela Pecuarista
            // ------------------------------------------------------------------------------------------------
            m.Entity<Pecuarista>().ToTable("Pecuarista");

            m.Entity<Pecuarista>().Property(p => p.PecuaristaId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            m.Entity<Pecuarista>().Property(p => p.Nome).IsUnicode(false).IsRequired().HasMaxLength(80);

            m.Entity<Pecuarista>().HasKey(e => e.PecuaristaId);
            m.Entity<Pecuarista>().HasIndex(p => p.Nome).HasName("IX_Pecuarista_Nome");

            // Tabela CompraGado
            // ------------------------------------------------------------------------------------------------
            m.Entity<CompraGado>().ToTable("CompraGado");

            m.Entity<CompraGado>().Property(p => p.CompraGadoId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            m.Entity<CompraGado>().Property(p => p.DataEntrega).IsRequired();
            m.Entity<CompraGado>().Property(p => p.PecuaristaId).IsRequired();

            m.Entity<CompraGado>().HasKey(e => e.CompraGadoId);
            m.Entity<CompraGado>().HasIndex(p => p.DataEntrega).HasName("IX_CompraGado_DataEntrega");
            m.Entity<CompraGado>().HasIndex(p => p.PecuaristaId).HasName("IX_CompraGado_Pecuarista");

            // Tabela CompraGadoItem
            // ------------------------------------------------------------------------------------------------
            m.Entity<CompraGadoItem>().ToTable("CompraGadoItem");

            m.Entity<CompraGadoItem>().Property(p => p.CompraGadoItemId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            m.Entity<CompraGadoItem>().Property(p => p.CompraGadoItemId).IsRequired();
            m.Entity<CompraGadoItem>().Property(p => p.AnimalId).IsRequired();
            m.Entity<CompraGadoItem>().Property(p => p.Quantidade).IsRequired();
            m.Entity<CompraGadoItem>().Property(p => p.Preco).IsRequired();

            m.Entity<CompraGadoItem>().HasKey(e => e.CompraGadoItemId);
            m.Entity<CompraGadoItem>().HasIndex(p => p.CompraGadoId).HasName("IX_CompraGadoItem_CompraGadoId");
            m.Entity<CompraGadoItem>().HasIndex(p => p.AnimalId).HasName("IX_CompraGadoItem_AnimalId");

            // Relacionamentos (1-N[0-*])
            // ------------------------------------------------------------------------------------------------

            m.Entity<CompraGado>()
                .HasRequired(e => e.Pecuarista)
                .WithMany(e => e.Compras)
                .HasForeignKey(e => e.PecuaristaId);

            m.Entity<CompraGadoItem>()
                .HasRequired(e => e.CompraGado)
                .WithMany(e => e.CompraGadoItens)
                .HasForeignKey(e => e.CompraGadoId);

            m.Entity<CompraGadoItem>()
                .HasRequired(e => e.Animal)
                .WithMany(e => e.CompraGadoItens)
                .HasForeignKey(e => e.AnimalId);

            base.OnModelCreating(m);

        }

        #endregion

        #region DbSet Tables

        public virtual DbSet<Animal> Animal { get; set; }

        public virtual DbSet<Pecuarista> Pecuarista { get; set; }

        public virtual DbSet<CompraGado> CompraGado { get; set; }

        public virtual DbSet<CompraGadoItem> CompraGadoItem { get; set; }

        #endregion

    }

}
