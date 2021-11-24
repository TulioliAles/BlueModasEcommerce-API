using LojaBlueModas_API.Dto;
using LojaBlueModas_API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LojaBlueModas_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class PedidoController : Controller
    {
        readonly IPedido _pedido;

        public PedidoController(IPedido pedido)
        {
            _pedido = pedido;
        }

        [HttpGet("{usuarioId}")]
        public async Task<List<PedidoDto>> Get(int usuarioId)
        {
            return await Task.FromResult(_pedido.GetPedidoList(usuarioId)).ConfigureAwait(true);
        }
    }
}
