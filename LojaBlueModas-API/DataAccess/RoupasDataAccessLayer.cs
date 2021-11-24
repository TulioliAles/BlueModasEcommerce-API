using LojaBlueModas_API.Dto;
using LojaBlueModas_API.Interfaces;
using LojaBlueModas_API.Models;
using LojaBlueModas_API.Models.BlueDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LojaBlueModas_API.DataAccess
{
    public class RoupasDataAccessLayer : IRoupas
    {
        readonly BlueDbContext _dbContext;

        public RoupasDataAccessLayer(BlueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Roupas> GetAllRoupas()
        {
            try
            {
                return _dbContext.Roupas.AsNoTracking().ToList();
            }
            catch
            {
                throw;
            }
        }

        public int AddRoupas(Roupas roupas)
        {
            try
            {
                _dbContext.Roupas.Add(roupas);
                _dbContext.SaveChanges();

                return 1;
            }
            catch
            {
                throw;
            }
        }

        public int UpdateRoupas(Roupas roupa)
        {
            try
            {
                Roupas oldRoupasData = GetRoupasData(roupa.RoupaId);

                if (oldRoupasData.Imagem != null)
                {
                    if (roupa.Imagem == null)
                    {
                        roupa.Imagem = oldRoupasData.Imagem;
                    }
                }

                _dbContext.Entry(roupa).State = EntityState.Modified;
                _dbContext.SaveChanges();

                return 1;
            }
            catch
            {
                throw;
            }
        }

        public Roupas GetRoupasData(int roupaId)
        {
            try
            {
                Roupas roupas = _dbContext.Roupas.FirstOrDefault(x => x.RoupaId == roupaId);
                if (roupas != null)
                {
                    _dbContext.Entry(roupas).State = EntityState.Detached;
                    return roupas;
                }
                return null;
            }
            catch
            {
                throw;
            }
        }

        public string DeleteRoupas(int roupasId)
        {
            try
            {
                Roupas roupa = _dbContext.Roupas.Find(roupasId);
                _dbContext.Roupas.Remove(roupa);
                _dbContext.SaveChanges();

                return (roupa.Imagem);
            }
            catch
            {
                throw;
            }
        }

        public List<Categorias> GetCategorias()
        {
            List<Categorias> lstCategorias = new List<Categorias>();
            lstCategorias = (from CategoriesList in _dbContext.Categorias select CategoriesList).ToList();

            return lstCategorias;
        }

        public List<Roupas> GetSimilarRoupas(int roupaId)
        {
            List<Roupas> lstRoupas = new List<Roupas>();
            Roupas roupa = GetRoupasData(roupaId);

            lstRoupas = _dbContext.Roupas.Where(x => x.Descricao == roupa.Descricao && x.RoupaId != roupa.RoupaId)
                .OrderBy(u => Guid.NewGuid())
                .Take(5)
                .ToList();
            return lstRoupas;
        }

        public List<CarrinhoItemDto> GetRoupasAvailableInCarrinho(string carrinhoID)
        {
            try
            {
                List<CarrinhoItemDto> carrinhoItemList = new List<CarrinhoItemDto>();
                List<CarrinhoItens> carrinhoItems = _dbContext.CarrinhoItens.Where(x => x.CarrinhoId == carrinhoID).ToList();

                foreach (CarrinhoItens item in carrinhoItems)
                {
                    Roupas roupa = GetRoupasData(item.ProdutoId);
                    CarrinhoItemDto objCarrinhoItem = new CarrinhoItemDto
                    {
                        Roupas = roupa,
                        Quantidade = item.Quantidade
                    };

                    carrinhoItemList.Add(objCarrinhoItem);
                }
                return carrinhoItemList;
            }
            catch
            {
                throw;
            }
        }

        public List<Roupas> GetRoupasAvailableInListaDesejos(string listaDesejosID)
        {
            try
            {
                List<Roupas> listaDesejo = new List<Roupas>();
                List<ListaDesejosItem> carrinhoItems = _dbContext.ListaDesejosItem.Where(x => x.ListaDesejosId == listaDesejosID).ToList();

                foreach (ListaDesejosItem item in carrinhoItems)
                {
                    Roupas roupas = GetRoupasData(item.ProdutoId);
                    listaDesejo.Add(roupas);
                }
                return listaDesejo;
            }
            catch
            {
                throw;
            }
        }
    }
}
