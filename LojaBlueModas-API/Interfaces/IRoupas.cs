using LojaBlueModas_API.Dto;
using LojaBlueModas_API.Models;
using System.Collections.Generic;

namespace LojaBlueModas_API.Interfaces
{
    public interface IRoupas
    {
        List<Roupas> GetAllRoupas();
        int AddRoupas(Roupas roupas);
        int UpdateRoupas(Roupas roupas);
        Roupas GetRoupasData(int roupasId);
        string DeleteRoupas(int roupasId);
        List<Categorias> GetCategorias();
        List<Roupas> GetSimilarRoupas(int roupasId);
        List<CarrinhoItemDto> GetRoupasAvailableInCarrinho(string carrinhoId);
        List<Roupas> GetRoupasAvailableInListaDesejos(string listaDesejosID);
    }
}
