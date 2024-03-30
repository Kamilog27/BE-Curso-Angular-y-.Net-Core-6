using BackEndCuestionario.Domain.IServices;
using BackEndCuestionario.Domain.Models;
using BackEndCuestionario.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackEndCuestionario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RespuestaCuestionarioController : ControllerBase
    {
        private readonly IRespuestaCuestionarioService respuestaCuestionarioService;
        private readonly ICuestionarioService cuestionarioService;

        public RespuestaCuestionarioController(IRespuestaCuestionarioService respuestaCuestionarioService,ICuestionarioService cuestionarioService)
        {
            this.respuestaCuestionarioService = respuestaCuestionarioService;
            this.cuestionarioService = cuestionarioService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RespuestaCuestionario respuestaCuestionario)
        {
            try
            {
                await respuestaCuestionarioService.SaveRespuestaCuestionario(respuestaCuestionario);
                return Ok(new { message = "" });
            }
            catch (Exception e)
            {

                return BadRequest(new { message = e.Message });
            }
        }
        [HttpGet("{idCuestionario}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get(int idCuestionario)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int idUsuario = JwtConfigurator.GetTokenIdUsuario(identity);
                var listRespuestaCuestionario = await this.respuestaCuestionarioService.ListRespuestaCuestionario(idCuestionario, idUsuario);
                if(listRespuestaCuestionario== null)
                {
                    return BadRequest(new { message = "Error al buscar el listado de respuestas" });

                }
                return Ok(listRespuestaCuestionario);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                
            }
        }
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int idUsuario = JwtConfigurator.GetTokenIdUsuario(identity);

                var respuestaCuestionario = await respuestaCuestionarioService.BuscarRespuestaCuestionario(id, idUsuario);

                if (respuestaCuestionario == null)
                {
                    return BadRequest(new { Message="Error al buscar la respuesta al cuestionario"});

                }
                await respuestaCuestionarioService.EliminarRespuestaCuestionario(respuestaCuestionario);
                return Ok(new {message="La respuesta al cuestionario fue eliminada con exito"});
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [Route("GetCuestionarioByIdRespuesta/{id}")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetCuestionarioByIdRespuesta(int id)
        {
            try
            {
               
                var idCuestionario = await respuestaCuestionarioService.GetIdCuestionarioByIdRespuesta(id);
                var cuestionario = await this.cuestionarioService.GetListCuestionario(idCuestionario);
                var listRespuesta = await respuestaCuestionarioService.GetListRespuestas(id);

                return Ok(new {cuestionario=cuestionario,respuestas=listRespuesta});
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);


            }
        }
    }
}
