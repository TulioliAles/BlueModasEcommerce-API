using LojaBlueModas_API.Interfaces;
using LojaBlueModas_API.Models;
using LojaBlueModas_API.Models.BlueDbContext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LojaBlueModas_API.DataAccess
{
    public class ListaDesejosDataAccessLayer : IListaDesejos
    {
        readonly BlueDbContext _dbContext;

        public ListaDesejosDataAccessLayer(BlueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void ToggleListaItem(int usuarioId, int roupaId)
        {
            string listaDesejosId = GetListaDesejosId(usuarioId);
            ListaDesejosItem existeListaDesejosItem = _dbContext.ListaDesejosItem.FirstOrDefault(x => x.ProdutoId == roupaId && x.ListaDesejosId == listaDesejosId);

            if (existeListaDesejosItem != null)
            {
                _dbContext.ListaDesejosItem.Remove(existeListaDesejosItem);
                _dbContext.SaveChanges();
            }
            else
            {
                ListaDesejosItem listaDesejosItem = new ListaDesejosItem
                {
                    ListaDesejosId = listaDesejosId,
                    ProdutoId = roupaId,
                };
                _dbContext.ListaDesejosItem.Add(listaDesejosItem);
                _dbContext.SaveChanges();
            }
        }

        public int ClearListaDesejos(int usuarioId)
        {
            try
            {
                string listaDesejosId = GetListaDesejosId(usuarioId);
                List<ListaDesejosItem> listaDesejosItem = _dbContext.ListaDesejosItem.Where(x => x.ListaDesejosId == listaDesejosId).ToList();

                if (!string.IsNullOrEmpty(listaDesejosId))
                {
                    foreach (ListaDesejosItem item in listaDesejosItem)
                    {
                        _dbContext.ListaDesejosItem.Remove(item);
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

        public string GetListaDesejosId(int usuarioId)
        {
            try
            {
                ListaDesejos lista = _dbContext.ListaDesejos.FirstOrDefault(x => x.UsuarioId == usuarioId);

                if (lista != null)
                {
                    return lista.ListaDesejosId;
                }
                else
                {
                    return CreateListaDesejos(usuarioId);
                }

            }
            catch
            {
                throw;
            }
        }

        string CreateListaDesejos(int usuarioId)
        {
            try
            {
                ListaDesejos lista = new ListaDesejos
                {
                    ListaDesejosId = Guid.NewGuid().ToString(),
                    UsuarioId = usuarioId,
                    DataCriacao = DateTime.Now.Date
                };

                _dbContext.ListaDesejos.Add(lista);
                _dbContext.SaveChanges();

                return lista.ListaDesejosId;
            }
            catch
            {
                throw;
            }
        }
    }
}
