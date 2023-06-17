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

        public PlanetaController(APIDbContext apiDBContext)
        {
            this.apiDBContext = apiDBContext;
            this.planetaData = new PlanetaData(apiDBContext);
        }

        [HttpGet]
        [Route("get")]
        public IActionResult GetPlanetas()
        {
            List<PlanetaAPIGet> planetasAPIGet = this.planetaData.getPlanetas();
            return Ok(planetasAPIGet);
        }

        [HttpGet]
        [Route("get/{Id}")]
        public IActionResult GetPlaneta([FromRoute] string Id)
        {
            try
            {
                PlanetaAPIGet planetaAPIGet = this.planetaData.getPlaneta(Id);
                return Ok(planetaAPIGet);
            }
            catch (Exception e)
            {
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
                return Ok(planeta);
            }
            catch (Exception e)
            {
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
                return Ok(planetaSeleccionado);
            }
            catch (Exception e)
            {
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
                return Ok(planeta);
            }
            catch
            {
                return BadRequest("No se logró eliminar el planeta solicitado.");
            }

        }

    }
}
