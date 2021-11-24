namespace LojaBlueModas_API.Models
{
    public partial class CarrinhoItens
    {
        public int CarrinhoItemId { get; set; }
        public string CarrinhoId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}
