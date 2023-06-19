using Microsoft.AspNetCore.Mvc;
using StarDeckAPI.Data;
using StarDeckAPI.Models;
using StarDeckAPI.Utilities;
using System.Numerics;

namespace StarDeckAPI.Controllers
{
    [ApiController]
    [Route("planeta")]
    public class PlanetaController : Controller
    {

        private APIDbContext apiDBContext;
        private PlanetaData planetaData;
        private readonly ILogger<CartaController> _logger;

        public PlanetaController(APIDbContext apiDBContext, ILogger<CartaController> logger)
        {
            this.apiDBContext = apiDBContext;
            this.planetaData = new PlanetaData(apiDBContext);
            _logger = logger;
        }

        [HttpGet]
        [Route("get")]
        public IActionResult GetPlanetas()
        {
            List<PlanetaAPIGet> planetasAPIGet = this.planetaData.getPlanetas();
            _logger.LogInformation("Se envio la informacion de los planetas correctamente");
            return Ok(planetasAPIGet);
        }

        [HttpGet]
        [Route("get/{Id}")]
        public IActionResult GetPlaneta([FromRoute] string Id)
        {
            try
            {
                PlanetaAPIGet planetaAPIGet = this.planetaData.getPlaneta(Id);
                _logger.LogInformation("Se envio la informacion del planeta correctamente");
                return Ok(planetaAPIGet);
            }
            catch (Exception e)
            {
                _logger.LogError("El planeta "+ Id + " no existe");
                return BadRequest("No se logró encontrar el planeta solicitado.");
            }
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddPlaneta(PlanetaAPI planetaAPI)
        {
            try
            {
                Planeta planeta = this.planetaData.addPlaneta(planetaAPI);
                _logger.LogInformation("Se creo el planeta correctamente");
                return Ok(planeta);
            }
            catch (Exception e)
            {
                _logger.LogError("No se logro crear el planeta");
                return BadRequest("No se logró añadir el planeta.");
            }
        }

        [HttpPut]
        [Route("update/{Id}")]
        public IActionResult UpdatePlaneta([FromRoute] string Id, PlanetaAPI planetaAPI)
        {
            try
            {
                Planeta planetaSeleccionado = this.planetaData.actualizarPlaneta(Id, planetaAPI);
                _logger.LogInformation("Se actualizo la informacion del planeta correctamente");
                return Ok(planetaSeleccionado);
            }
            catch (Exception e)
            {
                _logger.LogError("No se logro actualizar el planeta");
                return BadRequest("No se logró actualizar el planeta solicitado.");
            }
        }

        [HttpDelete]
        [Route("delete/{Id}")]
        public IActionResult DeletePlaneta([FromRoute] string Id)
        {
            try
            {
                Planeta planeta = this.planetaData.deletePlaneta(Id);
                _logger.LogInformation("Se borro el planeta "+Id);
                return Ok(planeta);
            }
            catch
            {
                _logger.LogError("No se logro crear el planeta");
                return BadRequest("No se logró eliminar el planeta solicitado.");
            }

        }

    }
}
