using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LojaBlueModas_API.Interfaces;
using LojaBlueModas_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LojaBlueModas_API.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        readonly IUsuario _usuario;
        readonly IConfiguration _config;

        public LoginController(IConfiguration config, IUsuario usuario)
        {
            _config = config;
            _usuario = usuario;
        }

        [HttpPost]
        public IActionResult Login([FromBody] Usuario login)
        {
            IActionResult response = Unauthorized();
            Usuario usuario = _usuario.AuthenticateUsuario(login);

            if (usuario != null)
            {
                var tokenString = GenerateJSONWebToken(usuario);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = usuario,
                });
            }

            return response;
        }

        string GenerateJSONWebToken(Usuario usuarioInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuarioInfo.Username),
                new Claim("usuarioId", usuarioInfo.UsuarioId.ToString(CultureInfo.InvariantCulture)),
                new Claim("usuarioTipoId", usuarioInfo.UsuarioTipoId.ToString(CultureInfo.InvariantCulture)),
                new Claim(ClaimTypes.Role,usuarioInfo.UsuarioTipoId.ToString(CultureInfo.InvariantCulture)),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
