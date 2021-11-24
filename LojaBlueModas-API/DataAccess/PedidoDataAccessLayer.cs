using LojaBlueModas_API.Dto;
using LojaBlueModas_API.Interfaces;
using LojaBlueModas_API.Models;
using LojaBlueModas_API.Models.BlueDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LojaBlueModas_API.DataAccess
{
    public class PedidoDataAccessLayer : IPedido
    {
        readonly BlueDbContext _dbContext;
        public PedidoDataAccessLayer(BlueDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void CreatePedido(int usuarioId, PedidoDto pedidoDetalhes)
        {
            try
            {
                StringBuilder pedidoId = new StringBuilder();
                pedidoId.Append(CreateRandomNumber(3));
                pedidoId.Append('-');
                pedidoId.Append(CreateRandomNumber(6));

                Pedido pedido = new Pedido
                {
                    PedidoId = pedidoId.ToString(),
                    UsuarioId = usuarioId,
                    DataCriacao = DateTime.Now.Date,
                    CarrinhoTotal = pedidoDetalhes.CarrinhoTotal
                };
                _dbContext.Pedido.Add(pedido);
                _dbContext.SaveChanges();

                foreach (CarrinhoItemDto carrinho in pedidoDetalhes.PedidoDetalhes)
                {
                    PedidoDetalhes produtoDetalhes = new PedidoDetalhes
                    {
                        PedidoId = pedidoId.ToString(),
                        ProdutoId = carrinho.Roupas.RoupaId,
                        Quantidade = carrinho.Quantidade,
                        Preco = carrinho.Roupas.Preco
                    };
                    _dbContext.PedidoDetalhes.Add(produtoDetalhes);
                    _dbContext.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }

        public List<PedidoDto> GetPedidoList(int usuarioId)
        {
            List<PedidoDto> usuarioPedidos = new List<PedidoDto>();
            List<string> usuarioPedidoId = new List<string>();

            usuarioPedidoId = _dbContext.Pedido.Where(x => x.UsuarioId == usuarioId)
                .Select(x => x.PedidoId).ToList();

            foreach (string pedidoId in usuarioPedidoId)
            {
                PedidoDto pedido = new PedidoDto
                {
                    PedidoId = pedidoId,
                    CarrinhoTotal = _dbContext.Pedido.FirstOrDefault(x => x.PedidoId == pedidoId).CarrinhoTotal,
                    PedidoData = _dbContext.Pedido.FirstOrDefault(x => x.PedidoId == pedidoId).DataCriacao
                };

                List<PedidoDetalhes> pedidoDetalhes= _dbContext.PedidoDetalhes.Where(x => x.PedidoId == pedidoId).ToList();

                pedido.PedidoDetalhes = new List<CarrinhoItemDto>();

                foreach (PedidoDetalhes ped in pedidoDetalhes)
                {
                    CarrinhoItemDto item = new CarrinhoItemDto();

                    Roupas roupa = new Roupas
                    {
                        RoupaId = ped.ProdutoId,
                        Descricao = _dbContext.Roupas.FirstOrDefault(x => x.RoupaId == ped.ProdutoId && ped.PedidoId == pedidoId).Descricao,
                        Preco = _dbContext.PedidoDetalhes.FirstOrDefault(x => x.ProdutoId == ped.ProdutoId && ped.PedidoId == pedidoId).Preco
                    };

                    item.Roupas = roupa;
                    item.Quantidade = _dbContext.PedidoDetalhes.FirstOrDefault(x => x.ProdutoId == ped.ProdutoId && x.PedidoId == pedidoId).Quantidade;

                    pedido.PedidoDetalhes.Add(item);
                }
                usuarioPedidos.Add(pedido);
            }
            return usuarioPedidos.OrderByDescending(x => x.PedidoData).ToList();
        }

        int CreateRandomNumber(int length)
        {
            Random rnd = new Random();
            return rnd.Next(Convert.ToInt32(Math.Pow(10, length - 1)), Convert.ToInt32(Math.Pow(10, length)));
        }
    }
}
