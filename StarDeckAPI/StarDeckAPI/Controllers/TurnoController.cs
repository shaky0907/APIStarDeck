using Microsoft.AspNetCore.Mvc;
using StarDeckAPI.Data;
using StarDeckAPI.Models;
using StarDeckAPI.Utilities;

namespace StarDeckAPI.Controllers
{
    [ApiController]
    [Route("Turno")]
    public class TurnoController : Controller
    {
        private APIDbContext apiDBContext;
        private TurnoData turnoData;
        public TurnoController(APIDbContext apiDBContext)
        {
            this.apiDBContext = apiDBContext;
            this.turnoData = new TurnoData(apiDBContext);
        }

        [HttpGet]
        [Route("getmano/{Id_usuario}/{Id_turno}")]
        public IActionResult GetUserMano([FromRoute] string Id_usuario, [FromRoute] string Id_turno)
        {

            List<string> cartas_Ids = this.turnoData.getUserMano(Id_usuario, Id_turno);
            return Ok(cartas_Ids);
        }

        [HttpGet]
        [Route("getdeck/{Id_usuario}/{Id_turno}")]
        public IActionResult GetUserDeck([FromRoute] string Id_usuario, [FromRoute] string Id_turno)
        {

            List<string> cartas_Ids = this.turnoData.getUserDeck(Id_usuario, Id_turno);
            return Ok(cartas_Ids);
        }

        [HttpGet]
        [Route("getcartasplaneta/{Id_planeta}/{Id_turno}/{Id_usuario}")]
        public IActionResult GetPlanetaCartas([FromRoute] string Id_planeta, [FromRoute] string Id_turno, [FromRoute] string Id_usuario)
        {

            List<string> cartas_Ids = this.turnoData.getPlanetaCartas(Id_planeta, Id_turno, Id_usuario);
            return Ok(cartas_Ids);
        }

        [HttpPost]
        [Route("addCartaTurnoManoUsuario")]
        public IActionResult AddCartaMano(CartasXTurnoXManoXUsuario cartasxturnoxmanoxusuario)
        {

            CartasXTurnoXManoXUsuario cxtxmxu = this.turnoData.addCartaMano(cartasxturnoxmanoxusuario);
            return Ok(cxtxmxu);

        }

        [HttpPost]
        [Route("addCartaTurnoDeckUsuario")]
        public IActionResult AddCartaDeck(CartasXTurnoXDeckXUsuario cartasxturnoxdeckxusuario)
        {

            CartasXTurnoXDeckXUsuario cxtxdxu = this.turnoData.AddCartaDeck(cartasxturnoxdeckxusuario);
            return Ok(cartasxturnoxdeckxusuario);

        }

        [HttpPost]
        [Route("addCartaTurnoPlanetaUsuario")]
        public IActionResult AddCartaDeck(CartasXTurnoXPlanetaXUsuario cartasXTurnoXPlanetaXUsuario)
        {

            CartasXTurnoXPlanetaXUsuario cxtxdxu = this.turnoData.AddCartaPlaneta(cartasXTurnoXPlanetaXUsuario);
            return Ok(cartasXTurnoXPlanetaXUsuario);

        }

        [HttpDelete]
        [Route("deleteCartaMano/{Id_Usuario}/{Id_Turno}/{Id_Carta}")]
        public IActionResult deleteCartaMano([FromRoute] string Id_Usuario, [FromRoute] string Id_Turno, [FromRoute] string Id_Carta)
        {
            CartasXTurnoXManoXUsuario deletedCartas = this.turnoData.deleteCartaMano(Id_Usuario, Id_Turno, Id_Carta);
            return Ok(deletedCartas);
        }

        [HttpDelete]
        [Route("deleteCartaDeck/{Id_Usuario}/{Id_Turno}/{Id_Carta}")]
        public IActionResult deleteCartaDeck([FromRoute] string Id_Usuario, [FromRoute] string Id_Turno, [FromRoute] string Id_Carta)
        {
            CartasXTurnoXDeckXUsuario deletedCartas = this.turnoData.deleteCartaDeck(Id_Usuario, Id_Turno, Id_Carta);
            return Ok(deletedCartas);
        }

        [HttpDelete]
        [Route("deleteCartaPlaneta/{Id_Usuario}/{Id_Turno}/{Id_Carta}")]
        public IActionResult deleteCartaPlaneta([FromRoute] string Id_Usuario, [FromRoute] string Id_Turno, [FromRoute] string Id_Carta)
        {
            CartasXTurnoXPlanetaXUsuario deletedCartas = this.turnoData.deleteCartaPlaneta(Id_Usuario, Id_Turno, Id_Carta);
            return Ok(deletedCartas);
        }

        [HttpPost]
        [Route("addTurno")]
        public IActionResult AddTurno(TurnoAPI turnoApi)
        {
            TurnoXUsuario turno = this.turnoData.addTurno(turnoApi);
            return Ok(turno);

        }

        [HttpGet]
        [Route("getLastTurno/{Id_Partida}/{Id_Usuario}")]
        public IActionResult getLastTurno([FromRoute] string Id_Partida, [FromRoute] string Id_Usuario)
        {
            TurnoAPI turno = this.turnoData.getLastTurno(Id_Partida, Id_Usuario);
            return Ok(turno);
        }

        [HttpGet]
        [Route("getTurno/{Id}")]
        public IActionResult getTurno([FromRoute] string Id)
        {
            return Ok(this.turnoData.getTurno(Id));
        }

        [HttpPut]
        [Route("updateTurno/{Id}")]
        public IActionResult updateTurno([FromRoute] string Id, TurnoAPI turnoApi)
        {
            TurnoXUsuario turno = this.turnoData.actualizarTurno(Id, turnoApi); 
            return Ok(turno);
        }

        [HttpPut]
        [Route("updateEnergia/{Id}/{Id_Usuario}")]
        public IActionResult updateTurno([FromRoute] string Id, [FromRoute] string Id_Usuario,int energia)
        {
            TurnoXUsuario turno = this.turnoData.actualizarEnergia(Id, Id_Usuario, energia);
            return Ok(turno);
        }

    }
}
