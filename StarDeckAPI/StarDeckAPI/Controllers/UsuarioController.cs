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
        private readonly ILogger<CartaController> _logger;

        public UsuarioController(APIDbContext apiDBContext, ILogger<CartaController> logger)
        {
            this.apiDBContext = apiDBContext;
            this.usuarioData = new UsuarioData(apiDBContext);
            _logger = logger;
        }

        [HttpGet]
        [Route("lista")]
        public IActionResult GetAll()
        {
            List<UsuarioAPI> list_return = this.usuarioData.getUsuarios();
            _logger.LogInformation("Se envio la informacion de los usuarios correctamente");
            return Ok(list_return);
        }

        [HttpGet]
        [Route("lista/jugadores")]
        public IActionResult GetAllPlayers()
        {
            List<UsuarioAPI> list_return = this.usuarioData.getJugadores();
            _logger.LogInformation("Se envio la informacion de los jugadores correctamente");
            return Ok(list_return);
        }

        [HttpGet]
        [Route("get/{Id}")]
        public IActionResult Get([FromRoute] string Id)
        {
            try
            {
                UsuarioAPI uApi = this.usuarioData.getUsuario(Id);
                _logger.LogInformation("Se envio la informacion del usuario correctamente");
                return Ok(uApi);
            }
            catch (Exception e)
            {
                _logger.LogError("No se logró obtener la información de usuario.");
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
                _logger.LogInformation("Se guardo el usuario correctamente");
                return Ok(usuario);
            }
            catch (Exception e)
            {
                _logger.LogError("No se logró guardar el jugador");
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
                _logger.LogInformation("Se borro el usuario correctamente");
                return Ok(usuarioDelete);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest("No se logró eliminar el usuario.");
            }
        }
    }
}
