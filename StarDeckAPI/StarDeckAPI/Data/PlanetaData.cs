using Microsoft.AspNetCore.Mvc;
using StarDeckAPI.Models;
using StarDeckAPI.Utilities;

namespace StarDeckAPI.Data
{
    public class PlanetaData
    {
        private APIDbContext apiDBContext;

        public PlanetaData(APIDbContext apiDBContext)
        {
            this.apiDBContext = apiDBContext;

        }

        public List<PlanetaAPIGet> getPlanetas()
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

            return planetasAPIGet;
        }

        public PlanetaAPIGet getPlaneta(string Id)
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
            return planetaAPIGet;
        }

        public Planeta addPlaneta(PlanetaAPI planetaAPI)
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

            return planeta;
        }

        public Planeta actualizarPlaneta(string Id, PlanetaAPI planetaAPI)
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

            }
            return planetaSeleccionado;
        }

        public Planeta deletePlaneta(String Id)
        {
            List<Planeta> planetaL = apiDBContext.Planeta.ToList();
            Planeta planeta = planetaL.Where(x => x.Id == Id).First();

            if (planeta != null)
            {
                apiDBContext.Planeta.Remove(planeta);
                apiDBContext.SaveChanges();
                
            }

            return planeta;
        }
    }
}
