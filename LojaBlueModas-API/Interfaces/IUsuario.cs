using LojaBlueModas_API.Models;

namespace LojaBlueModas_API.Interfaces
{
    public interface IUsuario
    {
        Usuario AuthenticateUsuario(Usuario loginCredenciais);
        int RegistroUsuario(Usuario usuarioData);
        bool CheckUsuarioDisponivel(string usuarioNome);
        bool isUsuarioExiste(int usuarioId);
    }
}
