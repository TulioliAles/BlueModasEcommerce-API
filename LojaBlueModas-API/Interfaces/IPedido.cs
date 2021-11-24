using LojaBlueModas_API.Dto;
using System.Collections.Generic;

namespace LojaBlueModas_API.Interfaces
{
    public interface IPedido
    {
        void CreatePedido(int usuarioId, PedidoDto pedidoDetalhes);
        List<PedidoDto> GetPedidoList(int usuarioId);
    }
}

