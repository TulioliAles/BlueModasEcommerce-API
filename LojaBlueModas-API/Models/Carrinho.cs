using System;

namespace LojaBlueModas_API.Models
{
    public partial class Carrinho
    {
        public string CarrinhoId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
