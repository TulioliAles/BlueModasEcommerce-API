using LojaBlueModas_API.Interfaces;
using LojaBlueModas_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LojaBlueModas_API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RoupaController : Controller
    {
        readonly IWebHostEnvironment _hostingEnvironment;
        readonly IRoupas _roupa;
        readonly IConfiguration _config;
        readonly string coverImageFolderPath = string.Empty;

        public RoupaController(IConfiguration config, IWebHostEnvironment hostingEnvironment, IRoupas roupa)
        {
            _config = config;
            _roupa = roupa;
            _hostingEnvironment = hostingEnvironment;
            coverImageFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "Upload");
            if (!Directory.Exists(coverImageFolderPath))
            {
                Directory.CreateDirectory(coverImageFolderPath);
            }
        }

        [HttpGet]
        public async Task<List<Roupas>> Get()
        {
            return await Task.FromResult(_roupa.GetAllRoupas()).ConfigureAwait(true);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Roupas roupa = _roupa.GetRoupasData(id);
            if (roupa != null)
            {
                return Ok(roupa);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("GetCategoriasLista")]
        public async Task<IEnumerable<Categorias>> CategoriaDetalhe()
        {
            return await Task.FromResult(_roupa.GetCategorias()).ConfigureAwait(true);
        }

        [HttpGet]
        [Route("GetSimilarRoupas/{roupaId}")]
        public async Task<List<Roupas>> SimilarRoupas(int roupaId)
        {
            return await Task.FromResult(_roupa.GetSimilarRoupas(roupaId)).ConfigureAwait(true);
        }

        [HttpPost, DisableRequestSizeLimit]
        [Authorize(Policy = UsuarioRegra.Admin)]
        public int Post()
        {
            Roupas roupa = JsonConvert.DeserializeObject<Roupas>(Request.Form["roupaFormData"].ToString());

            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files[0];

                if (file.Length > 0)
                {
                    string fileName = Guid.NewGuid() + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(coverImageFolderPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    roupa.Imagem = fileName;
                }
            }
            else
            {
                roupa.Imagem = _config["DefaultCoverImageFile"];
            }
            return _roupa.AddRoupas(roupa);
        }

        [HttpPut]
        [Authorize(Policy = UsuarioRegra.Admin)]
        public int Put()
        {
            Roupas roupa = JsonConvert.DeserializeObject<Roupas>(Request.Form["roupaFormData"].ToString());
            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files[0];

                if (file.Length > 0)
                {
                    string fileName = Guid.NewGuid() + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(coverImageFolderPath, fileName);
                    bool isFileExists = Directory.Exists(fullPath);

                    if (!isFileExists)
                    {
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        roupa.Imagem = fileName;
                    }
                }
            }
            return _roupa.UpdateRoupas(roupa);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = UsuarioRegra.Admin)]
        public int Delete(int id)
        {
            string coverFileName = _roupa.DeleteRoupas(id);
            if (coverFileName != _config["DefaultCoverImageFile"])
            {
                string fullPath = Path.Combine(coverImageFolderPath, coverFileName);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            return 1;
        }
    }
}
