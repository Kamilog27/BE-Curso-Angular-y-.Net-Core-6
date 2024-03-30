using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEndCuestionario.Controllers
{
    public class DefaultController : Controller
    {
        // GET: DefaultController
        [HttpGet]
        public string Get()
        {
            return "Aplicación Corriendo...";
        }

       
    }
}
