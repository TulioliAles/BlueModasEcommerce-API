using System;
using System.Collections.Generic;

namespace LojaBlueModas_API.Dto
{
    public class PedidoDto
    {
        public string PedidoId { get; set; }
        public List<CarrinhoItemDto> PedidoDetalhes { get; set; }
        public decimal CarrinhoTotal { get; set; }
        public DateTime PedidoData { get; set; }
    }
}
