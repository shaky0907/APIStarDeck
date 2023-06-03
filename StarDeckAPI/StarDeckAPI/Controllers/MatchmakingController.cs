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
            MatchmakingResponse matchmakingResponse =  this.matchmakingData.matchmakingCheck(Id);

            return Ok(matchmakingResponse);
        }


        [HttpPut]
        [Route("searchGame/{Id}")]
        public IActionResult searchGame([FromRoute] string Id)
        {
            Usuario user = this.matchmakingData.buscarPartida(Id);
            return Ok(user);

        }

        [HttpPut]
        [Route("finishMatchUser/{Id}")]
        public IActionResult finishMatchUser([FromRoute] string Id)
        {
            Usuario user = this.matchmakingData.terminarPartida(Id);
            return Ok(user);

        }

        [HttpPut]
        [Route("finishGame/{Id}")]
        public IActionResult finishGame([FromRoute] string Id)
        {
            Partida partida = this.matchmakingData.finalizarJuego(Id);
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
            List<Planeta> planetas = this.matchmakingData.getPlanetasPartida(Id);
            return Ok(planetas);

        }
        [HttpGet]
        [Route("getRival/{Id_usuario}/{Id_Partida}")]
        public IActionResult getRival([FromRoute] string Id_usuario,[FromRoute] string Id_Partida)
        {
            Usuario rivalUsuario = this.matchmakingData.getRival(Id_usuario, Id_Partida);
            return Ok(rivalUsuario);
        }

        [HttpGet]
        [Route("isInMatch/{Id_usuario}")]
        public IActionResult getIsInMatch([FromRoute] string Id_usuario)
        {
            Partida partida = this.matchmakingData.isInMatch(Id_usuario);
            return Ok(partida);
        }
    }
}
