using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LojaBlueModas_API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carrinho",
                columns: table => new
                {
                    CarrinhoId = table.Column<string>(unicode: false, maxLength: 36, nullable: false),
                    UsuarioID = table.Column<int>(nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrinho", x => x.CarrinhoId);
                });

            migrationBuilder.CreateTable(
                name: "CarrinhoItens",
                columns: table => new
                {
                    CarrinhoItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarrinhoId = table.Column<string>(unicode: false, maxLength: 36, nullable: false),
                    ProdutoId = table.Column<int>(nullable: false),
                    Quantidade = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CarrinhoItem__488B0B0AA0297D1C", x => x.CarrinhoItemId);
                });

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CategoriaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoriaNome = table.Column<string>(unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categoria__19093A2B46B8DFC9", x => x.CategoriaID);
                });

            migrationBuilder.CreateTable(
                name: "ListaDesejos",
                columns: table => new
                {
                    ListaDesejosId = table.Column<string>(unicode: false, maxLength: 36, nullable: false),
                    UsuarioID = table.Column<int>(nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListaDesejos", x => x.ListaDesejosId);
                });

            migrationBuilder.CreateTable(
                name: "ListaDesejosItem",
                columns: table => new
                {
                    ListaDesejosItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListaDesejosId = table.Column<string>(unicode: false, maxLength: 36, nullable: false),
                    ProdutoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ListaDesejos__171E21A16A5148A4", x => x.ListaDesejosItemId);
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    PedidoId = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    UsuarioID = table.Column<int>(nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    CarrinhoTotal = table.Column<decimal>(type: "decimal(10, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pedido__C3905BCF96C8F1E7", x => x.PedidoId);
                });

            migrationBuilder.CreateTable(
                name: "PedidoDetalhes",
                columns: table => new
                {
                    PedidoDetalhesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoId = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    ProdutoId = table.Column<int>(nullable: false),
                    Quantidade = table.Column<int>(nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(10, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pedido__9DD74DBD81D9221B", x => x.PedidoDetalhesId);
                });

            migrationBuilder.CreateTable(
                name: "Roupas",
                columns: table => new
                {
                    RoupaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Preco = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    Imagem = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roupa__220D0G1JA8314F2H", x => x.RoupaID);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    UsuarioID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Sobrenome = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Username = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Senha = table.Column<string>(unicode: false, maxLength: 40, nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    UsuarioTipoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuario__1788CCAC2694A2ED", x => x.UsuarioID);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioTipo",
                columns: table => new
                {
                    UsuarioTipoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioTipoNome = table.Column<string>(unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioTipo", x => x.UsuarioTipoID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carrinho");

            migrationBuilder.DropTable(
                name: "CarrinhoItens");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "ListaDesejos");

            migrationBuilder.DropTable(
                name: "ListaDesejosItem");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "PedidoDetalhes");

            migrationBuilder.DropTable(
                name: "Roupas");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "UsuarioTipo");
        }
    }
}
