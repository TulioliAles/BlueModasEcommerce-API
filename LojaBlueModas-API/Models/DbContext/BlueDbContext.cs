using Microsoft.EntityFrameworkCore;

namespace LojaBlueModas_API.Models.BlueDbContext
{
    public partial class BlueDbContext : DbContext
    {
        public BlueDbContext()
        {
        }

        public BlueDbContext(DbContextOptions<BlueDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Roupas> Roupas { get; set; }
        public virtual DbSet<Carrinho> Carrinho { get; set; }
        public virtual DbSet<CarrinhoItens> CarrinhoItens { get; set; }
        public virtual DbSet<Categorias> Categorias { get; set; }
        public virtual DbSet<PedidoDetalhes> PedidoDetalhes { get; set; }
        public virtual DbSet<Pedido> Pedido { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<UsuarioTipo> UsuarioTipo { get; set; }
        public virtual DbSet<ListaDesejos> ListaDesejos { get; set; }
        public virtual DbSet<ListaDesejosItem> ListaDesejosItem { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("A FALLBACK CONNECTION STRING");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Roupas>(entity =>
            {
                entity.HasKey(e => e.RoupaId)
                    .HasName("PK__Roupa__220D0G1JA8314F2H");

                entity.Property(e => e.RoupaId).HasColumnName("RoupaID");

                entity.Property(e => e.Descricao)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Imagem)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Preco).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<Carrinho>(entity =>
            {
                entity.Property(e => e.CarrinhoId)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.DataCriacao).HasColumnType("datetime");

                entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            });

            modelBuilder.Entity<CarrinhoItens>(entity =>
            {
                entity.HasKey(e => e.CarrinhoItemId)
                    .HasName("PK__CarrinhoItem__488B0B0AA0297D1C");

                entity.Property(e => e.CarrinhoId)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Categorias>(entity =>
            {
                entity.HasKey(e => e.CategoriaId)
                    .HasName("PK__Categoria__19093A2B46B8DFC9");

                entity.Property(e => e.CategoriaId).HasColumnName("CategoriaID");

                entity.Property(e => e.CategoriaNome)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PedidoDetalhes>(entity =>
            {
                entity.HasKey(e => e.PedidoDetalhesId)
                    .HasName("PK__Pedido__9DD74DBD81D9221B");

                entity.Property(e => e.PedidoId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Preco).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.PedidoId)
                    .HasName("PK__Pedido__C3905BCF96C8F1E7");

                entity.Property(e => e.PedidoId)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CarrinhoTotal).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.DataCriacao).HasColumnType("datetime");

                entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.UsuarioId)
                    .HasName("PK__Usuario__1788CCAC2694A2ED");

                entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Sobrenome)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioTipoId).HasColumnName("UsuarioTipoID");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UsuarioTipo>(entity =>
            {
                entity.Property(e => e.UsuarioTipoId).HasColumnName("UsuarioTipoID");

                entity.Property(e => e.UsuarioTipoNome)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ListaDesejos>(entity =>
            {
                entity.Property(e => e.ListaDesejosId)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.DataCriacao).HasColumnType("datetime");

                entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            });

            modelBuilder.Entity<ListaDesejosItem>(entity =>
            {
                entity.HasKey(e => e.ListaDesejosItemId)
                    .HasName("PK__ListaDesejos__171E21A16A5148A4");

                entity.Property(e => e.ListaDesejosId)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
