using LojaBlueModas_API.Interfaces;
using LojaBlueModas_API.Models;
using LojaBlueModas_API.Models.BlueDbContext;
using System.Linq;

namespace LojaBlueModas_API.DataAccess
{
    public class UsuarioDataAccessLayer : IUsuario
    {
        readonly BlueDbContext _dbContext;

        public UsuarioDataAccessLayer(BlueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Usuario AuthenticateUsuario(Usuario loginCredenciais)
        {
            Usuario usuario = new Usuario();

            var usuarioDetalhes = _dbContext.Usuario.FirstOrDefault(
                u => u.Username == loginCredenciais.Nome && u.Senha == loginCredenciais.Senha
                );

            if (usuarioDetalhes != null)
            {

                usuario = new Usuario
                {
                    Username = usuarioDetalhes.Username,
                    UsuarioId = usuarioDetalhes.UsuarioId,
                    UsuarioTipoId = usuarioDetalhes.UsuarioTipoId
                };
                return usuario;
            }
            else
            {
                return usuarioDetalhes;
            }
        }

        public int RegistroUsuario(Usuario usuario)
        {
            try
            {
                usuario.UsuarioTipoId = 2;
                _dbContext.Usuario.Add(usuario);
                _dbContext.SaveChanges();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public bool CheckUsuarioDisponivel(string userName)
        {
            string usuario = _dbContext.Usuario.FirstOrDefault(x => x.Username == userName)?.ToString();

            if (usuario != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool isUsuarioExiste(int usuarioId)
        {
            string usuario = _dbContext.Usuario.FirstOrDefault(x => x.UsuarioId == usuarioId)?.ToString();

            if (usuario != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
