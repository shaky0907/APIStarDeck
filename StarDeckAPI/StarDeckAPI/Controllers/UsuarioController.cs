using Microsoft.AspNetCore.Mvc;
using StarDeckAPI.Data;
using StarDeckAPI.Models;
using StarDeckAPI.Utilities;
using System.Numerics;
using System.Text.RegularExpressions;

namespace StarDeckAPI.Controllers
{
    [ApiController]
    [Route("usuario")]

    public class UsuarioController : Controller
    {
        private APIDbContext apiDBContext;
        private UsuarioData usuarioData;

        public UsuarioController(APIDbContext apiDBContext)
        {
            this.apiDBContext = apiDBContext;
            this.usuarioData = new UsuarioData(apiDBContext);
        }

        [HttpGet]
        [Route("lista")]
        public IActionResult GetAll()
        {
            List<UsuarioAPI> list_return = this.usuarioData.getUsuarios();
            return Ok(list_return);
        }

        [HttpGet]
        [Route("lista/jugadores")]
        public IActionResult GetAllPlayers()
        {
            List<UsuarioAPI> list_return = this.usuarioData.getJugadores();
            return Ok(list_return);
        }

        [HttpGet]
        [Route("get/{Id}")]
        public IActionResult Get([FromRoute] string Id)
        {
            try
            {
                UsuarioAPI uApi = this.usuarioData.getUsuario(Id);
                return Ok(uApi);
            }
            catch (Exception e)
            {
                return BadRequest("No se logró obtener la información de usuario.");
            }
        }

        [HttpPost]
        [Route("guardarJugador")]
        public IActionResult SaveUsuario(UsuarioAPI usuarioAPI)
        {
            try
            {
                Usuario usuario = this.usuarioData.addUsuario(usuarioAPI);
                return Ok(usuario);
            }
            catch (Exception e)
            {
                return BadRequest("No se logró guardar el jugador.");
            }
        }

        [HttpDelete]
        [Route("delete/{Id}")]
        public IActionResult DeleteUsuario([FromRoute] string Id)
        {
            try
            {
                Usuario usuarioDelete = this.usuarioData.deleteUsuario(Id);
                return Ok(usuarioDelete);
            }
            catch (Exception e)
            {
                return BadRequest("No se logró eliminar el usuario.");
            }
        }
    }
}
