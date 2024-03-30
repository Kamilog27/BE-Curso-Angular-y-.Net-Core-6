using BackEndCuestionario.Domain.IServices;
using BackEndCuestionario.Domain.Models;
using BackEndCuestionario.DTO;
using BackEndCuestionario.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackEndCuestionario.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService usuarioService;


        public UsuarioController(IUsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Usuario usuario)
        {
            try
            {
                var validateExistence = await usuarioService.ValidateExistence(usuario);
                if (validateExistence)
                {
                    return BadRequest(new { message = "El usuario " + usuario.NombreUsuario + " ya existe" });
                }
                usuario.Contraseña = Encriptar.EncriptarPassword(usuario.Contraseña);
                await this.usuarioService.SaveUser(usuario);

                return Ok(new { message = "Usuario registrado con éxito" });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
        }
        [Route("CambiarPassword")]
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        public async Task<IActionResult> CambiarPassword([FromBody] CambiarPasswordDTO cambiarPassword)
        {
            try
            {
                var identity=HttpContext.User.Identity as ClaimsIdentity;
                int idUsuario = JwtConfigurator.GetTokenIdUsuario(identity);
                string passwordEncriptado = Encriptar.EncriptarPassword(cambiarPassword.passwordAnterior);
                var usuario = await usuarioService.ValidatePassword(idUsuario, passwordEncriptado);
                if (usuario == null)
                {
                    return BadRequest(new { message = "Password es incorrecta" });
                }
                else
                {
                    usuario.Contraseña = Encriptar.EncriptarPassword(cambiarPassword.nuevaPassword);
                    await usuarioService.UpdatePassword(usuario);
                    return Ok(new { message = "La password fue actualizada con éxito!" });
                }
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }


    }
}
