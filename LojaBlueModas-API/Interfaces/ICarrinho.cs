namespace LojaBlueModas_API.Interfaces
{
    public interface ICarrinho
    {
        void AddRoupaToCarrinho(int usuarioId, int roupaId);
        void RemoveCarrinhoItem(int usuarioId, int roupaId);
        void DeleteOneCarrinhoItem(int usuarioId, int bookId);
        int GetCarrinhoItemCount(int usuarioId);
        void MergeCarrinho(int tempUserId, int permUserId);
        int ClearCarrinho(int usuarioId);
        string GetCarrinhoId(int usuarioId);
    }
}
