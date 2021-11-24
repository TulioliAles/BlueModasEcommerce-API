using LojaBlueModas_API.Interfaces;
using LojaBlueModas_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace LojaBlueModas_API.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        readonly IUsuario _usuario;
        readonly ICarrinho _carrinho;

        public UsuarioController(IUsuario usuario, ICarrinho carrinho)
        {
            _usuario = usuario;
            _carrinho = carrinho;
        }

        [HttpGet("{usuarioId}")]
        public int Get(int usuarioId)
        {
            int carrinhoItemCount = _carrinho.GetCarrinhoItemCount(usuarioId);
            return carrinhoItemCount;
        }

        [HttpGet]
        [Route("validaUserName/{userName}")]
        public bool ValidateUserName(string userName)
        {
            return _usuario.CheckUsuarioDisponivel(userName);
        }

        [HttpPost]
        public void Post([FromBody] Usuario userData)
        {
            _usuario.RegistroUsuario(userData);
        }
    }
}
