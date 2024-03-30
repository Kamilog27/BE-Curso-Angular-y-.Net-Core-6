using BackEndCuestionario.Domain.IServices;
using BackEndCuestionario.Domain.Models;
using BackEndCuestionario.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BackEndCuestionario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILoginService loginService;
        private readonly IConfiguration configuration;

        public LoginController(ILoginService loginService, IConfiguration configuration)
        {
            this.loginService = loginService;
            this.configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult>Post([FromBody]Usuario usuario)
        {
            try
            {
                usuario.Contraseña = Encriptar.EncriptarPassword(usuario.Contraseña);
                var user = await loginService.ValidateUser(usuario);
                if (user == null)
                {
                    return BadRequest(new { message = "Usuario o contraseña invalidos" });
                }
                string tokenString = JwtConfigurator.GetToken(user,configuration);
                return Ok(new { token=  tokenString,usuario=user.NombreUsuario });
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
