namespace LojaBlueModas_API.Models
{
    public partial class PedidoDetalhes
    {
        public int PedidoDetalhesId { get; set; }
        public string PedidoId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
    }
}
