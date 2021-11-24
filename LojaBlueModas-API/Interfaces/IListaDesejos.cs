namespace LojaBlueModas_API.Interfaces
{
    public interface IListaDesejos
    {
        void ToggleListaItem(int usuarioId, int roupaId);
        int ClearListaDesejos(int usuarioId);
        string GetListaDesejosId(int usuarioId);
    }
}
