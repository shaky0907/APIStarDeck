using Microsoft.AspNetCore.Mvc;
using StarDeckAPI.Data;
using StarDeckAPI.Models;

namespace StarDeckAPI.Controllers
{
    [ApiController]
    [Route("usuario")]

    public class UsuarioController : Controller
    {
        private readonly APIDbContext apiDBContext;
        private Random random = new Random();

        public UsuarioController(APIDbContext apiDBContext)
        {
            this.apiDBContext = apiDBContext;
        }

        [HttpGet]
        [Route("lista")]
        public IActionResult GetAll()
        {
            List<Usuario> list_usuario = apiDBContext.Usuario.ToList();
            List<UsuarioAPI> list_return = new List<UsuarioAPI>();
            foreach (Usuario usuario in list_usuario)
            {

            }

            return Ok();
        }
    }
}
