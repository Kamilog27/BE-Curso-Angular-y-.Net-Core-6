using BackEndCuestionario.Domain.IServices;
using BackEndCuestionario.Domain.Models;
using BackEndCuestionario.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackEndCuestionario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuestionarioController : Controller
    {
        private readonly ICuestionarioService cuestionarioService;

        public CuestionarioController(ICuestionarioService cuestionarioService)
        {
            this.cuestionarioService = cuestionarioService;
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Post([FromBody] Cuestionario cuestionario)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int idUsuario = JwtConfigurator.GetTokenIdUsuario(identity);
                cuestionario.UsuarioId = idUsuario;
                cuestionario.Activo = 1;
                cuestionario.FechaCreacion = DateTime.Now;
                await cuestionarioService.CreateCuestionario(cuestionario);
                return Ok(new { message = "Cuestionario Creado" });
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [Route("GetListCuestionarioByUser")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetListCuestinarioByUser()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int idUsuario = JwtConfigurator.GetTokenIdUsuario(identity);
                var listCuestionario = await cuestionarioService.GetListCuestionarioByUser(idUsuario);
                return Ok(listCuestionario );
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("{idCuestionario}")]
        public async Task<IActionResult> GetListCuestinario(int idCuestionario)
        {
            try
            {
                
                var cuestionario = await cuestionarioService.GetListCuestionario(idCuestionario);
                return Ok( cuestionario );
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpDelete("{idCuestionario}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(int idCuestionario)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int idUsuario = JwtConfigurator.GetTokenIdUsuario(identity);
                var cuestionario = await cuestionarioService.BuscarCuestionario(idCuestionario, idUsuario);
                if (cuestionario == null)
                {
                    return BadRequest(new { message = "Cuestionario no encontrado" });

                }
                else
                {
                    await cuestionarioService.EliminarCuestionario(cuestionario);
                    return Ok(new { message = "Cuestionario Eliminado" });

                }
            }
            catch (Exception e)
            {

                return BadRequest(new { message = e.Message });
            }
        }
        [Route("GetListCuestionarios")]
        [HttpGet]
        public async Task<IActionResult> GetListCuestionarios()
        {
            try
            {
          
                var listCuestionarios = await cuestionarioService.GetListCuestionarios();
                if (listCuestionarios == null)
                {
                    return BadRequest(new { message = "Cuestionario no encontrado o no activo" });

                }
                else
                {
                               
                    return Ok( listCuestionarios );
                }
            }
            catch (Exception e)
            {

                return BadRequest(new { message = e.Message });
            }
        }
    }
}
