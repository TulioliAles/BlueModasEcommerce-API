using LojaBlueModas_API.Interfaces;
using LojaBlueModas_API.Models;
using LojaBlueModas_API.Models.BlueDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LojaBlueModas_API.DataAccess
{
    public class CarrinhoDataAccessLayer : ICarrinho
    {
        readonly BlueDbContext _dbContext;

        public CarrinhoDataAccessLayer(BlueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddRoupaToCarrinho(int usuarioId, int roupaId)
        {
            string carrinhoId = GetCarrinhoId(usuarioId);
            int quantidade = 1;

            CarrinhoItens existeCarrinhoItem = _dbContext.CarrinhoItens.FirstOrDefault(x => x.ProdutoId == roupaId && x.CarrinhoId == carrinhoId);

            if (existeCarrinhoItem != null)
            {
                existeCarrinhoItem.Quantidade += 1;
                _dbContext.Entry(existeCarrinhoItem).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            else
            {
                CarrinhoItens carrinhoItens = new CarrinhoItens
                {
                    CarrinhoId = carrinhoId,
                    ProdutoId = roupaId,
                    Quantidade = quantidade
                };
                _dbContext.CarrinhoItens.Add(carrinhoItens);
                _dbContext.SaveChanges();
            }
        }

        public string GetCarrinhoId(int usuarioId)
        {
            try
            {
                Carrinho carrinho = _dbContext.Carrinho.FirstOrDefault(x => x.UsuarioId == usuarioId);

                if (carrinho != null)
                {
                    return carrinho.CarrinhoId;
                }
                else
                {
                    return CriaCarrinho(usuarioId);
                }

            }
            catch
            {
                throw;
            }
        }

        string CriaCarrinho(int usuarioId)
        {
            try
            {
                Carrinho shoppingCarrinho = new Carrinho
                {
                    CarrinhoId = Guid.NewGuid().ToString(),
                    UsuarioId = usuarioId,
                    DataCriacao = DateTime.Now.Date
                };

                _dbContext.Carrinho.Add(shoppingCarrinho);
                _dbContext.SaveChanges();

                return shoppingCarrinho.CarrinhoId;
            }
            catch
            {
                throw;
            }
        }

        public void RemoveCarrinhoItem(int usuarioId, int roupaId)
        {
            try
            {
                string carrinhoId = GetCarrinhoId(usuarioId);
                CarrinhoItens carrinhoItem = _dbContext.CarrinhoItens.FirstOrDefault(x => x.ProdutoId == roupaId && x.CarrinhoId == carrinhoId);

                _dbContext.CarrinhoItens.Remove(carrinhoItem);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void DeleteOneCarrinhoItem(int usuarioId, int roupaId)
        {
            try
            {
                string carrinhoId = GetCarrinhoId(usuarioId);
                CarrinhoItens carrinhoItem = _dbContext.CarrinhoItens.FirstOrDefault(x => x.ProdutoId == roupaId && x.CarrinhoId == carrinhoId);

                carrinhoItem.Quantidade -= 1;
                _dbContext.Entry(carrinhoItem).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public int GetCarrinhoItemCount(int usuarioId)
        {
            string carrinhoId = GetCarrinhoId(usuarioId);

            if (!string.IsNullOrEmpty(carrinhoId))
            {
                int carrinhoItemCount = _dbContext.CarrinhoItens.Where(x => x.CarrinhoId == carrinhoId).Sum(x => x.Quantidade);

                return carrinhoItemCount;
            }
            else
            {
                return 0;
            }
        }

        public void MergeCarrinho(int tempUsuarioId, int permUsuarioId)
        {
            try
            {
                if (tempUsuarioId != permUsuarioId && tempUsuarioId > 0 && permUsuarioId > 0)
                {
                    string tempCarrinhoId = GetCarrinhoId(tempUsuarioId);
                    string permCarrinhoId = GetCarrinhoId(permUsuarioId);

                    List<CarrinhoItens> tempCartItems = _dbContext.CarrinhoItens.Where(x => x.CarrinhoId == tempCarrinhoId).ToList();

                    foreach (CarrinhoItens item in tempCartItems)
                    {
                        CarrinhoItens carrinhoItem = _dbContext.CarrinhoItens.FirstOrDefault(x => x.ProdutoId == item.ProdutoId && x.CarrinhoId == permCarrinhoId);

                        if (carrinhoItem != null)
                        {
                            carrinhoItem.Quantidade += item.Quantidade;
                            _dbContext.Entry(carrinhoItem).State = EntityState.Modified;
                        }
                        else
                        {
                            CarrinhoItens newCarrinhoItem = new CarrinhoItens
                            {
                                CarrinhoId = permCarrinhoId,
                                ProdutoId = item.ProdutoId,
                                Quantidade = item.Quantidade
                            };
                            _dbContext.CarrinhoItens.Add(newCarrinhoItem);
                        }
                        _dbContext.CarrinhoItens.Remove(item);
                        _dbContext.SaveChanges();
                    }
                    DeleteCarrinho(tempCarrinhoId);
                }
            }
            catch
            {
                throw;
            }
        }

        public int ClearCarrinho(int usuarioId)
        {
            try
            {
                string carrinhoId = GetCarrinhoId(usuarioId);
                List<CarrinhoItens> carrinhoItem = _dbContext.CarrinhoItens.Where(x => x.CarrinhoId == carrinhoId).ToList();

                if (!string.IsNullOrEmpty(carrinhoId))
                {
                    foreach (CarrinhoItens item in carrinhoItem)
                    {
                        _dbContext.CarrinhoItens.Remove(item);
                        _dbContext.SaveChanges();
                    }
                }
                return 0;
            }
            catch
            {
                throw;
            }
        }

        void DeleteCarrinho(string carrinhoId)
        {
            Carrinho carrinho = _dbContext.Carrinho.Find(carrinhoId);
            _dbContext.Carrinho.Remove(carrinho);
            _dbContext.SaveChanges();
        }
    }
}
