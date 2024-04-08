using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Sicoob.API.Auth.Models;

namespace Sicoob.API.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        //[Authorize]
        [RequiredScope("Forecast.Read")]
        [HttpGet("GetUsuario")]
        public ActionResult<Response<string>> GetUsuario()
        {
            Response<string> response = new Response<string>();
            response.Mensagem = "Acessei";

            return Ok(response);
        }

    }
}