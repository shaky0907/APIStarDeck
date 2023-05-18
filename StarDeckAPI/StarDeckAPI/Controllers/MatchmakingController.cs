using Microsoft.AspNetCore.Mvc;
using StarDeckAPI.Data;
using StarDeckAPI.Models;
using StarDeckAPI.Utilities;
using System.Reflection.Metadata.Ecma335;

namespace StarDeckAPI.Controllers
{
    [ApiController]
    [Route("matchmaking")]
    public class MatchmakingController : Controller
    {
        private readonly APIDbContext apiDBContext;
        //private List<Planeta> planetas_partida;

        public MatchmakingController(APIDbContext apiDBContext)
        {
            this.apiDBContext = apiDBContext;
        }

        [HttpGet]
        [Route("matchmakingCheck/{Id}")]
        public IActionResult matchmakingCheck([FromRoute] string Id)
        {
            MatchmakingResponse matchmakingResponse = new MatchmakingResponse();
            List<UsuarioXPartida> uxpL = apiDBContext.UsuarioXPartida.ToList().Where(x => x.Id_Usuario == Id).ToList();
            int myUserCheck = 0;
            bool continue_check = false;

            if (apiDBContext.Usuario.ToList().Where(x => x.Id == Id).ToList().Any())
            {
                continue_check = true;
                myUserCheck = apiDBContext.Usuario.ToList().Where(x => x.Id == Id).First().Id_actividad;
            }

            if (continue_check)
            {
                if (uxpL.Any())
                {
                    bool alreadypaired = false;
                    bool alreadystarted = false;
                    foreach (UsuarioXPartida uxp in uxpL)
                    {
                        Partida partida = apiDBContext.Partida.ToList().Where(x => x.Id == uxp.Id_Partida).First();
                        bool uxp_check_master = apiDBContext.UsuarioXPartida.ToList().Where(x => x.Id_Usuario == Id).First().Id_Master;

                        if ((partida.Estado == 1) && (!uxp_check_master))
                        {
                            partida.Estado = 2;
                            alreadypaired = true;
                            apiDBContext.Update(partida);
                            apiDBContext.SaveChanges();
                            matchmakingResponse.Id_Partida = partida.Id;
                            break;



                        }
                        else if ((partida.Estado == 2) && (uxp_check_master))
                        {
                            partida.Estado = 3;
                            alreadystarted = true;
                            apiDBContext.Update(partida);
                            apiDBContext.SaveChanges();
                            matchmakingResponse.Id_Partida = partida.Id;
                            break;
                        }

                    }

                    if (alreadypaired)
                    {
                        matchmakingResponse.estado = 2;
                    }
                    else if (alreadystarted)
                    {
                        matchmakingResponse.estado = 3;
                    }

                    else if (myUserCheck == 2)
                    {

                        matchmakingResponse = this.createNewMatch(Id);
                    }
                    else
                    {
                        matchmakingResponse.estado = 4;
                    }
                }
                else
                {

                    matchmakingResponse = this.createNewMatch(Id);


                }
            }
            matchmakingResponse.Id = Id;

            return Ok(matchmakingResponse);
        }


        private MatchmakingResponse createNewMatch(string Id)
        {

            Usuario myUser = apiDBContext.Usuario.ToList().Where(x => x.Id == Id).First();
            MatchmakingResponse matchmakingResponse = new MatchmakingResponse();
            List<Usuario> usuarios = apiDBContext.Usuario.ToList();
            bool foundanotheruser = false;
            string idFoundUser = "";
            foreach (Usuario user in usuarios)
            {
                if ((user.Id != Id) && (user.Id_actividad == 2))
                {
                    if ((user.Ranking > myUser.Ranking - 50) && (user.Ranking < myUser.Ranking + 50))
                    {
                        idFoundUser = user.Id;
                        user.Id_actividad = 3;
                        myUser.Id_actividad = 3;
                        apiDBContext.Update(user);
                        apiDBContext.Update(myUser);
                        apiDBContext.SaveChanges();
                        foundanotheruser = true;
                        break;
                    }
                }
            }
            if (foundanotheruser)
            {
                string idPartida = GeneratorID.GenerateRandomId("G-");
                matchmakingResponse.estado = 1;
                Partida partidanueva = new Partida()
                {
                    Id = idPartida,
                    Estado = 1,
                    Fecha_hora = DateTime.Now
                };
                matchmakingResponse.Id_Partida = idPartida;
                apiDBContext.Add(partidanueva);
                apiDBContext.SaveChanges();

                UsuarioXPartida uxpnew1 = new UsuarioXPartida()
                {
                    Id_Partida = idPartida,
                    Id_Usuario = Id,
                    Id_Master = true
                };

                UsuarioXPartida uxpnew2 = new UsuarioXPartida()
                {
                    Id_Partida = idPartida,
                    Id_Usuario = idFoundUser,
                    Id_Master = false
                };

                apiDBContext.Add(uxpnew1);
                apiDBContext.Add(uxpnew2);
                apiDBContext.SaveChanges();

                int i = 0;
                int planetsLeft = 3;
                Random rand = new Random();
                List<Planeta> planetasSelect = apiDBContext.Planeta.ToList();
                List<Tipo_planeta> probabilidades = apiDBContext.Tipo_planeta.ToList();
                List<Planeta> planetas_partida = new List<Planeta>();

                while (planetsLeft > 0)
                {
                    if ((rand.NextDouble() < ((float)(probabilidades.Where(x => x.Id == planetasSelect.ElementAt(i).Tipo).First().Probabilidad)) / (float)100.0) &&
                        !(planetas_partida.Contains(planetasSelect.ElementAt(i))))
                    {
                        planetas_partida.Add(planetasSelect.ElementAt(i));
                        planetsLeft--;
                        PlanetasXPartida pxp = new PlanetasXPartida()
                        {
                            Id_Partida = idPartida,
                            Id_Planeta = planetasSelect.ElementAt(i).Id
                        };
                        apiDBContext.Add(pxp);
                    }
                    i++;



                    if (i == planetasSelect.Count)
                    {
                        i = 0;
                    }
                }
                apiDBContext.SaveChanges();
            }
            else
            {
                matchmakingResponse.estado = 0;

            }

            return matchmakingResponse;
        }


        [HttpPut]
        [Route("searchGame/{Id}")]
        public IActionResult searchGame([FromRoute] string Id)
        {
            Usuario user = apiDBContext.Usuario.ToList().Where(x => x.Id == Id).First();
            user.Id_actividad = 2;
            apiDBContext.Update(user);
            apiDBContext.SaveChanges();
            return Ok(user);

        }

        [HttpPut]
        [Route("finishMatch/{Id}")]
        public IActionResult finishMatchUser([FromRoute] string Id)
        {
            Usuario user = apiDBContext.Usuario.ToList().Where(x => x.Id == Id).First();
            user.Id_actividad = 1;
            apiDBContext.Update(user);
            apiDBContext.SaveChanges();
            return Ok(user);

        }

        [HttpPut]
        [Route("finishGame/{Id}")]
        public IActionResult finishGame([FromRoute] string Id)
        {
            Partida partida = apiDBContext.Partida.ToList().Where(x => x.Id == Id).First();
            partida.Estado = 4;
            apiDBContext.Update(partida);
            apiDBContext.SaveChanges();
            return Ok(partida);

        }

        [HttpGet]
        [Route("getPartida/{Id}")]

        public IActionResult getPartida([FromRoute] string Id)
        {
            Partida partida = apiDBContext.Partida.ToList().Where(x => x.Id == Id).First();
            return Ok(partida);
        }

        [HttpGet]
        [Route("getPlanetasPartida/{Id}")]
        public IActionResult getPlanetasPartida([FromRoute] string Id)
        {
            List<PlanetasXPartida> pxps = apiDBContext.PlanetasXPartida.ToList().Where(x => x.Id_Partida == Id).ToList();
            List<Planeta> planetas = new List<Planeta>();

            foreach (PlanetasXPartida pxp in pxps)
            {
                Planeta p = new Planeta()
                {
                    Id = pxp.Id_Planeta,
                    Nombre = apiDBContext.Planeta.ToList().Where(x => x.Id == pxp.Id_Planeta).First().Nombre,
                    Tipo = apiDBContext.Planeta.ToList().Where(x => x.Id == pxp.Id_Planeta).First().Tipo,
                    Descripcion = apiDBContext.Planeta.ToList().Where(x => x.Id == pxp.Id_Planeta).First().Descripcion,
                    Estado = apiDBContext.Planeta.ToList().Where(x => x.Id == pxp.Id_Planeta).First().Estado,
                    Imagen = apiDBContext.Planeta.ToList().Where(x => x.Id == pxp.Id_Planeta).First().Imagen

                };
                planetas.Add(p);
            }

            return Ok(planetas);

        }
    }
}
