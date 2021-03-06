// <auto-generated />
using System;
using LojaBlueModas_API.Models.BlueDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LojaBlueModas_API.Migrations
{
    [DbContext(typeof(BlueDbContext))]
    [Migration("20211115132901_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LojaBlueModas_API.Models.Carrinho", b =>
                {
                    b.Property<string>("CarrinhoId")
                        .HasColumnType("varchar(36)")
                        .HasMaxLength(36)
                        .IsUnicode(false);

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime");

                    b.Property<int>("UsuarioId")
                        .HasColumnName("UsuarioID")
                        .HasColumnType("int");

                    b.HasKey("CarrinhoId");

                    b.ToTable("Carrinho");
                });

            modelBuilder.Entity("LojaBlueModas_API.Models.CarrinhoItens", b =>
                {
                    b.Property<int>("CarrinhoItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CarrinhoId")
                        .IsRequired()
                        .HasColumnType("varchar(36)")
                        .HasMaxLength(36)
                        .IsUnicode(false);

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.HasKey("CarrinhoItemId")
                        .HasName("PK__CarrinhoItem__488B0B0AA0297D1C");

                    b.ToTable("CarrinhoItens");
                });

            modelBuilder.Entity("LojaBlueModas_API.Models.Categorias", b =>
                {
                    b.Property<int>("CategoriaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CategoriaID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoriaNome")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.HasKey("CategoriaId")
                        .HasName("PK__Categoria__19093A2B46B8DFC9");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("LojaBlueModas_API.Models.ListaDesejos", b =>
                {
                    b.Property<string>("ListaDesejosId")
                        .HasColumnType("varchar(36)")
                        .HasMaxLength(36)
                        .IsUnicode(false);

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime");

                    b.Property<int>("UsuarioId")
                        .HasColumnName("UsuarioID")
                        .HasColumnType("int");

                    b.HasKey("ListaDesejosId");

                    b.ToTable("ListaDesejos");
                });

            modelBuilder.Entity("LojaBlueModas_API.Models.ListaDesejosItem", b =>
                {
                    b.Property<int>("ListaDesejosItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ListaDesejosId")
                        .IsRequired()
                        .HasColumnType("varchar(36)")
                        .HasMaxLength(36)
                        .IsUnicode(false);

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.HasKey("ListaDesejosItemId")
                        .HasName("PK__ListaDesejos__171E21A16A5148A4");

                    b.ToTable("ListaDesejosItem");
                });

            modelBuilder.Entity("LojaBlueModas_API.Models.Pedido", b =>
                {
                    b.Property<string>("PedidoId")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<decimal>("CarrinhoTotal")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime");

                    b.Property<int>("UsuarioId")
                        .HasColumnName("UsuarioID")
                        .HasColumnType("int");

                    b.HasKey("PedidoId")
                        .HasName("PK__Pedido__C3905BCF96C8F1E7");

                    b.ToTable("Pedido");
                });

            modelBuilder.Entity("LojaBlueModas_API.Models.PedidoDetalhes", b =>
                {
                    b.Property<int>("PedidoDetalhesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PedidoId")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.HasKey("PedidoDetalhesId")
                        .HasName("PK__Pedido__9DD74DBD81D9221B");

                    b.ToTable("PedidoDetalhes");
                });

            modelBuilder.Entity("LojaBlueModas_API.Models.Roupas", b =>
                {
                    b.Property<int>("RoupaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("RoupaID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("Imagem")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("RoupaId")
                        .HasName("PK__Roupa__220D0G1JA8314F2H");

                    b.ToTable("Roupas");
                });

            modelBuilder.Entity("LojaBlueModas_API.Models.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("UsuarioID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("varchar(40)")
                        .HasMaxLength(40)
                        .IsUnicode(false);

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("Telefone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<int>("UsuarioTipoId")
                        .HasColumnName("UsuarioTipoID")
                        .HasColumnType("int");

                    b.HasKey("UsuarioId")
                        .HasName("PK__Usuario__1788CCAC2694A2ED");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("LojaBlueModas_API.Models.UsuarioTipo", b =>
                {
                    b.Property<int>("UsuarioTipoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("UsuarioTipoID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UsuarioTipoNome")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.HasKey("UsuarioTipoId");

                    b.ToTable("UsuarioTipo");
                });
#pragma warning restore 612, 618
        }
    }
}
