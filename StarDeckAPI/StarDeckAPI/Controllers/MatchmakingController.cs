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
        private APIDbContext apiDBContext;
        //private List<Planeta> planetas_partida;
        private MatchmakingData matchmakingData;
        public MatchmakingController(APIDbContext apiDBContext)
        {
            this.apiDBContext = apiDBContext;
            this.matchmakingData = new MatchmakingData(apiDBContext);
        }

        [HttpGet]
        [Route("matchmakingCheck/{Id}")]
        public IActionResult matchmakingCheck([FromRoute] string Id)
        {
            try
            {
                MatchmakingResponse matchmakingResponse =  this.matchmakingData.matchmakingCheck(Id);
                return Ok(matchmakingResponse);
            }
            catch (Exception e)
            {
                return BadRequest("No se logró encontrar el usuario solicitado en la partida.");
            }
        }


        [HttpPut]
        [Route("searchGame/{Id}")]
        public IActionResult searchGame([FromRoute] string Id)
        {
            try
            {
                Usuario user = this.matchmakingData.buscarPartida(Id);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest("No se logró encontrar la partida.");
            }

        }

        [HttpPut]
        [Route("finishMatchUser/{Id}/{matchId}")]
        public IActionResult finishMatchUser([FromRoute] string Id, [FromRoute] string matchId)
        {
            try
            {
                Usuario user = this.matchmakingData.terminarPartida(Id, matchId);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest("No se logró finalizar la partida para el usuario.");
            }

        }

        [HttpPut]
        [Route("finishMatchSearch/{Id}")]
        public IActionResult finishMatchSearch([FromRoute] string Id)
        {
            try
            {
                Usuario user = this.matchmakingData.terminarBusquedaPartida(Id);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest("No se logró finalizar la partida para el usuario.");
            }

        }

        [HttpPut]
        [Route("finishGame/{Id}")]
        public IActionResult finishGame([FromRoute] string Id)
        {
            try
            {
                Partida partida = this.matchmakingData.finalizarJuego(Id);
                return Ok(partida);
            }
            catch (Exception e)
            {
                return BadRequest("No se logró finalizar la partida.");
            }

        }

        [HttpGet]
        [Route("getPartida/{Id}")]

        public IActionResult getPartida([FromRoute] string Id)
        {
            try
            {
                if (Id != "null") 
                {
                    Partida partida = apiDBContext.Partida.ToList().Where(x => x.Id == Id).First();
                    return Ok(partida);
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest("No se logró encontrar la partida.");
            }
        }

        [HttpGet]
        [Route("getPlanetasPartida/{Id}")]
        public IActionResult getPlanetasPartida([FromRoute] string Id)
        {
            try
            {
            List<Planeta> planetas = this.matchmakingData.getPlanetasPartida(Id);
            return Ok(planetas);
            }
            catch (Exception e)
            {
                return BadRequest("No se logró obtener la lista de planetas de la partida.");
            }
        }
        [HttpGet]
        [Route("getRival/{Id_usuario}/{Id_Partida}")]
        public IActionResult getRival([FromRoute] string Id_usuario,[FromRoute] string Id_Partida)
        {
            try
            {
                Usuario rivalUsuario = this.matchmakingData.getRival(Id_usuario, Id_Partida);
                return Ok(rivalUsuario);
            }
            catch (Exception e)
            {
                return BadRequest("No se logró obtener el rival.");
            }
        }

        [HttpGet]
        [Route("isInMatch/{Id_usuario}")]
        public IActionResult getIsInMatch([FromRoute] string Id_usuario)
        {
            try
            {   
                Partida partida = this.matchmakingData.isInMatch(Id_usuario);
                return Ok(partida);
            }
            catch (Exception e)
            {
                return BadRequest("No se logró verificar si el usuario está en partida.");
            }
        }
    }
}
