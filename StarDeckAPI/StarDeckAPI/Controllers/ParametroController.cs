using Microsoft.AspNetCore.Mvc;
using StarDeckAPI.Data;

namespace StarDeckAPI.Controllers
{
    [ApiController]
    [Route("Parametros")]
    public class ParametroController : Controller
    {
        private APIDbContext apiDBContext;
        private readonly ILogger<CartaController> _logger;

        public ParametroController(APIDbContext apiDBContext, ILogger<CartaController> logger)
        {
            this.apiDBContext = apiDBContext;
            _logger = logger;
        }
        [HttpGet]
        [Route("getParametros")]
        public IActionResult getParametros()
        {
            try
            {
                _logger.LogInformation("Se envio la informacion de los parametros correctamente");
                return Ok(this.apiDBContext.Parametros.ToList());
            }
            catch (Exception e)
            {
                _logger.LogError("No se lograron enviar los parametros");
                return BadRequest("No se logró obtener los parametros.");
            }
        }
    }
}
