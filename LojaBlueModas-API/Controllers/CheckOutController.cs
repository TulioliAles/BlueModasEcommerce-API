using LojaBlueModas_API.Dto;
using LojaBlueModas_API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaBlueModas_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CheckOutController : Controller
    {
        readonly IPedido _pedido;
        readonly ICarrinho _carrinho;

        public CheckOutController(IPedido pedido, ICarrinho carrinho)
        {
            _pedido = pedido;
            _carrinho = carrinho;
        }

        [HttpPost("{usuarioId}")]
        public int Post(int usuarioId, [FromBody] PedidoDto saidaItens)
        {
            _pedido.CreatePedido(usuarioId, saidaItens);
            return _carrinho.ClearCarrinho(usuarioId);
        }
    }
}
