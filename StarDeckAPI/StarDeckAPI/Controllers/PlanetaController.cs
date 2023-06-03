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
            PlanetaAPIGet planetaAPIGet = this.planetaData.getPlaneta(Id);
            return Ok(planetaAPIGet);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddPlaneta(PlanetaAPI planetaAPI)
        {

            Planeta planeta = this.planetaData.addPlaneta(planetaAPI);

            return Ok(planeta);

        }

        [HttpPut]
        [Route("update/{Id}")]
        public IActionResult UpdatePlaneta([FromRoute] string Id, PlanetaAPI planetaAPI)
        {
            Planeta planetaSeleccionado = this.planetaData.actualizarPlaneta(Id, planetaAPI);

            if (planetaSeleccionado != null)
            {
                return Ok(planetaSeleccionado);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("delete/{Id}")]
        public IActionResult DeletePlaneta([FromRoute] string Id)
        {
            Planeta planeta = this.planetaData.deletePlaneta(Id);

            if (planeta != null)
            {
                return Ok(planeta);
            }

            return NotFound();

        }

    }
}
