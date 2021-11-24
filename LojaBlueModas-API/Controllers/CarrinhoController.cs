
using LojaBlueModas_API.Dto;
using LojaBlueModas_API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LojaBlueModas_API.Controllers
{
    [Route("api/[controller]")]
    public class CarrinhoController : Controller
    {
        private readonly ICarrinho _carrinho;
        private readonly IRoupas _roupas;

        public CarrinhoController(ICarrinho carrinho, IRoupas roupas)
        {
            _roupas = roupas;
            _carrinho = carrinho;
        }

        [Authorize]
        [HttpGet]
        [Route("setcarrinho/{antigoUsuarioId}/{novaUsuarioId}")]
        public int Get(int antigoUsuarioId, int novaUsuarioId)
        {
            _carrinho.MergeCarrinho(antigoUsuarioId, novaUsuarioId);
            return _carrinho.GetCarrinhoItemCount(novaUsuarioId);
        }

        [HttpGet("{usuarioId}")]
        public async Task<List<CarrinhoItemDto>> Get(int usuarioId)
        {
            string carrinhoId = _carrinho.GetCarrinhoId(usuarioId);
            return await Task.FromResult(_roupas.GetRoupasAvailableInCarrinho(carrinhoId)).ConfigureAwait(true);
        }

        [HttpPost]
        [Route("adicionacarrinho/{usuarioId}/{roupaId}")]
        public int Post(int usuarioId, int roupaId)
        {
            _carrinho.AddRoupaToCarrinho(usuarioId, roupaId);
            return _carrinho.GetCarrinhoItemCount(usuarioId);
        }

        [HttpPut("{usuarioId}/{roupaId}")]
        public int Put(int usuarioId, int roupaId)
        {
            _carrinho.DeleteOneCarrinhoItem(usuarioId, roupaId);
            return _carrinho.GetCarrinhoItemCount(usuarioId);
        }

        [HttpDelete("{usuarioId}/{roupaId}")]
        public int Delete(int usuarioId, int roupaId)
        {
            _carrinho.RemoveCarrinhoItem(usuarioId, roupaId);
            return _carrinho.GetCarrinhoItemCount(usuarioId);
        }

        [HttpDelete("{usuarioId}")]
        public int Delete(int usuarioId)
        {
            return _carrinho.ClearCarrinho(usuarioId);
        }
    }
}
