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

        private readonly APIDbContext apiDBContext;

        public PlanetaController(APIDbContext apiDBContext)
        {
            this.apiDBContext = apiDBContext;
        }

        [HttpGet]
        [Route("get")]
        public IActionResult GetPlanetas()
        {
            List<Planeta> planetas = apiDBContext.Planeta.ToList();

            List<PlanetaAPIGet> planetasAPIGet = new List<PlanetaAPIGet>();

            foreach (Planeta planeta in planetas)
            {
                string tipo_planeta = apiDBContext.Tipo_planeta.ToList().Where(x => x.Id == planeta.Tipo).First().Nombre;
                PlanetaAPIGet planetaAPIGet = new PlanetaAPIGet()
                {
                    Id = planeta.Id,
                    Nombre = planeta.Nombre,
                    Tipo = tipo_planeta,
                    Descripcion = planeta.Descripcion,
                    Estado = planeta.Estado,
                    Imagen = planeta.Imagen

                };
                planetasAPIGet.Add(planetaAPIGet);
            }

            return Ok(planetasAPIGet);
        }

        [HttpGet]
        [Route("get/{Id}")]
        public IActionResult GetPlaneta([FromRoute] string Id)
        {
            Planeta planeta = apiDBContext.Planeta.ToList().Where(x => x.Id == Id).First();
            string tipo_planeta = apiDBContext.Tipo_planeta.ToList().Where(x => x.Id == planeta.Tipo).First().Nombre;
            PlanetaAPIGet planetaAPIGet = new PlanetaAPIGet()
            {
                Id = planeta.Id,
                Nombre = planeta.Nombre,
                Tipo = tipo_planeta,
                Descripcion = planeta.Descripcion,
                Estado = planeta.Estado,
                Imagen = planeta.Imagen

            };
            return Ok(planetaAPIGet);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddPlaneta(PlanetaAPI planetaAPI)
        {

            Planeta planeta = new Planeta()
            {
                Id = GeneratorID.GenerateRandomId("P-"),
                Nombre = planetaAPI.Nombre,
                Tipo = planetaAPI.Tipo,
                Descripcion = planetaAPI.Descripcion,
                Estado = planetaAPI.Estado,
                Imagen = planetaAPI.Imagen
            };
            apiDBContext.Planeta.Add(planeta);

            apiDBContext.SaveChanges();

            return Ok(planeta);

        }

        [HttpPut]
        [Route("update/{Id}")]
        public IActionResult UpdatePlaneta([FromRoute] string Id, PlanetaAPI planetaAPI)
        {
            List<Planeta> planetas = apiDBContext.Planeta.ToList();
            Planeta planetaSeleccionado = planetas.Where(x => x.Id == Id).First();

            if (planetaSeleccionado != null)
            {
                planetaSeleccionado.Id = Id;
                planetaSeleccionado.Nombre = planetaAPI.Nombre;
                planetaSeleccionado.Tipo = planetaAPI.Tipo;
                planetaSeleccionado.Descripcion = planetaAPI.Descripcion;
                planetaSeleccionado.Estado = planetaAPI.Estado;
                planetaSeleccionado.Imagen = planetaAPI.Imagen;


                apiDBContext.Planeta.Update(planetaSeleccionado);
                apiDBContext.SaveChanges();
                return Ok(planetaSeleccionado);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("delete/{Id}")]
        public IActionResult DeletePlaneta([FromRoute] string Id)
        {
            List<Planeta> planetaL = apiDBContext.Planeta.ToList();
            Planeta planeta = planetaL.Where(x => x.Id == Id).First();

            if (planeta != null)
            {
                apiDBContext.Planeta.Remove(planeta);
                apiDBContext.SaveChanges();
                return Ok(planeta);
            }

            return NotFound();

        }

    }
}
