using System;

namespace LojaBlueModas_API.Models
{
    public partial class Pedido
    {
        public string PedidoId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataCriacao { get; set; }
        public decimal CarrinhoTotal { get; set; }
    }
}
