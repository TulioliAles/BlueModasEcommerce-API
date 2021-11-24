using LojaBlueModas_API.Interfaces;
using LojaBlueModas_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LojaBlueModas_API.Controllers
{
    [Route("api/[controller]")]
    public class ListaDesejosController : Controller
    {
        readonly IListaDesejos _listaDesejos;
        readonly IRoupas _roupas;
        readonly IUsuario _usuario;

        public ListaDesejosController(IListaDesejos listaDesejos, IRoupas roupas, IUsuario usuario)
        {
            _listaDesejos = listaDesejos;
            _roupas = roupas;
            _usuario = usuario;
        }

        [HttpGet("{usuarioId}")]
        public async Task<List<Roupas>> Get(int usuarioId)
        {
            return await Task.FromResult(GetUsuarioListaDesejos(usuarioId)).ConfigureAwait(true);
        }

        [Authorize]
        [HttpPost]
        [Route("alteraListaDesejos/{usuarioId}/{roupaId}")]
        public async Task<List<Roupas>> Post(int usuarioId, int roupaId)
        {
            _listaDesejos.ToggleListaItem(usuarioId, roupaId);
            return await Task.FromResult(GetUsuarioListaDesejos(usuarioId)).ConfigureAwait(true);
        }

        
        [Authorize]
        [HttpDelete("{usuarioId}")]
        public int Delete(int usuarioId)
        {
            return _listaDesejos.ClearListaDesejos(usuarioId);
        }

        List<Roupas> GetUsuarioListaDesejos(int usuarioId)
        {
            bool usuario = _usuario.isUsuarioExiste(usuarioId);
            if (usuario)
            {
                string listaDesejosId = _listaDesejos.GetListaDesejosId(usuarioId);
                return _roupas.GetRoupasAvailableInListaDesejos(listaDesejosId);
            }
            else
            {
                return new List<Roupas>();
            }
        }
    }
}
