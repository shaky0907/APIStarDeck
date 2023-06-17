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
            try
            {
                List<CartaAPI> cartas_Ids = this.turnoData.getUserMano(Id_usuario, Id_turno);
                return Ok(cartas_Ids);
            }
            catch (Exception e)
            {
                return BadRequest("No se logro obtener la mano del usuario solicitado.");
            }
        }

        [HttpGet]
        [Route("getdeck/{Id_usuario}/{Id_turno}")]
        public IActionResult GetUserDeck([FromRoute] string Id_usuario, [FromRoute] string Id_turno)
        {
            try
            {
                List<CartaAPI> cartas_Ids = this.turnoData.getUserDeck(Id_usuario, Id_turno);
                return Ok(cartas_Ids);
            }
            catch (Exception e)
            {
                return BadRequest("No se logro obtener el deck del usuario en el turno solicitado.");
            }
        }

        [HttpGet]
        [Route("getcartasplaneta/{Id_planeta}/{Id_turno}/{Id_usuario}")]
        public IActionResult GetPlanetaCartas([FromRoute] string Id_planeta, [FromRoute] string Id_turno, [FromRoute] string Id_usuario)
        {
            try
            {
                List<CartaAPI> cartas_Ids = this.turnoData.getPlanetaCartas(Id_planeta, Id_turno, Id_usuario);
                return Ok(cartas_Ids);
            }
            catch (Exception e)
            {
                return BadRequest("No se logró obtener las cartas del planeta solicitado.");
            }
        }

        [HttpGet]
        [Route("getcartasplanetapartida/{Id_planeta}/{Id_partida}/{Id_usuario}")]
        public IActionResult GetPlanetaCartasPartida([FromRoute] string Id_planeta, [FromRoute] string Id_partida, [FromRoute] string Id_usuario)
        {
            try
            {
                List<CartaAPI> cartas_Ids = this.turnoData.getPlanetaCartasPartida(Id_planeta, Id_partida, Id_usuario);
                return Ok(cartas_Ids);
            }
            catch (Exception e)
            {
                return BadRequest("No se logró obtener las cartas del planeta en la partida solicitada.");
            }
            
        }

        [HttpGet]
        [Route("getGanador/{Id_partida}/{Id_usuario}/")]
        public IActionResult GetGanadorPlanetaPartida([FromRoute] string Id_partida, [FromRoute] string Id_usuario)
        {
            try
            {
                WinnerAPI ganador = this.turnoData.getGanadorPartida(Id_partida, Id_usuario);
                return Ok(ganador);
            }
            catch (Exception e)
            {
                return BadRequest("No se logró obtener el ganador de la partida solicitada.");
            }
        }

        [HttpGet]
        [Route("getInfoCompletaTurno/{Id_partida}/{Id_usuario}")]
        public async Task<IActionResult> getInfoCompletaTurno([FromRoute] string Id_partida, [FromRoute] string Id_usuario)
        {
            try
            {
                TurnoCompletoAPI turnoCompleto = await this.turnoData.getInfoCompletaUltimoTurno(Id_partida, Id_usuario);
                return Ok(turnoCompleto);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("No se logró obtener la información completa del turno.");
            }
        }

        [HttpPut]
        [Route("updateInfoCompletaTurno/{Id_partida}/{Id_usuario}")]
        public async Task<IActionResult> getInfoCompletaTurno([FromRoute] string Id_partida, [FromRoute] string Id_usuario, TurnoCompletoAPI turnoCompleto)
        {
            try
            {
                TurnoCompletoAPI turnoCompletoUpdated = await this.turnoData.updateInfoCompletaTurno(Id_partida, Id_usuario, turnoCompleto);
                return Ok(turnoCompletoUpdated);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("No se logró actualizar la informacion del turno.");
            }
        }

        [HttpPut]
        [Route("giveUpMatch/{Id_partida}/{Id_usuario}")]
        public IActionResult giveUpMatch([FromRoute] string Id_partida, [FromRoute] string Id_usuario)
        {
            try
            {
                UsuarioXPartida usuarioXPartida = this.turnoData.giveUpMatch(Id_partida, Id_usuario);
                return Ok(usuarioXPartida);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("No se logró rendirse de la partida.");
            }
        }

        [HttpPost]
        [Route("crearNuevoTurnoCompleto/{Id_partida}/{Id_usuario}")]
        public async Task<IActionResult> CrearNuevoTurnoCompleto([FromRoute] string Id_partida, [FromRoute] string Id_usuario, TurnoCompletoAPI turnoCompleto)
        {
            try
            {
                TurnoCompletoAPI turnoCompletoNuevo = await this.turnoData.CrearNuevoTurnoCompleto(Id_partida, Id_usuario, turnoCompleto);
                return Ok(turnoCompletoNuevo);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("No se logró crear el nuevo turno.");
            }
        }

        [HttpPost]
        [Route("addTurnoCompleto/{Id_partida}/{Id_usuario}")]
        public IActionResult AddTurnoCompleto([FromRoute] string Id_partida, [FromRoute] string Id_usuario, TurnoCompletoAPI turnoCompleto)
        {
            try
            {
                TurnoCompletoAPI turnoCompletoNuevo = this.turnoData.AddTurnoCompleto(Id_partida, Id_usuario, turnoCompleto);
                return Ok(turnoCompletoNuevo);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("No se logró crear el turno incial.");
            }
        }

        [HttpPost]
        [Route("addCartaTurnoManoUsuario")]
        public IActionResult AddCartaMano(CartasXTurnoXManoXUsuario cartasxturnoxmanoxusuario)
        {
            try
            {
                CartasXTurnoXManoXUsuario cxtxmxu = this.turnoData.addCartaMano(cartasxturnoxmanoxusuario);
                return Ok(cxtxmxu);
            }
            catch (Exception e)
            {
                return BadRequest("No se logró añadir la carta a la mano del usuario.");
            }
        }

        [HttpPost]
        [Route("addCartaTurnoDeckUsuario")]
        public IActionResult AddCartaDeck(CartasXTurnoXDeckXUsuario cartasxturnoxdeckxusuario)
        {
            try
            {   
                CartasXTurnoXDeckXUsuario cxtxdxu = this.turnoData.AddCartaDeck(cartasxturnoxdeckxusuario);
                return Ok(cartasxturnoxdeckxusuario);
            }
            catch (Exception e)
            {
                return BadRequest("No se logró añadir la carta al deck del usuario.");
            }
        }

        [HttpPost]
        [Route("addCartaTurnoPlanetaUsuario")]
        public IActionResult AddCartaPlaneta(CartasXTurnoXPlanetaXUsuario cartasXTurnoXPlanetaXUsuario)
        {
            try
            {
                CartasXTurnoXPlanetaXUsuario cxtxdxu = this.turnoData.AddCartaPlaneta(cartasXTurnoXPlanetaXUsuario);
                return Ok(cartasXTurnoXPlanetaXUsuario);
            }
            catch (Exception e)
            {
                return BadRequest("");
            }
        }

        [HttpDelete]
        [Route("deleteCartaMano/{Id_Usuario}/{Id_Turno}/{Id_Carta}")]
        public IActionResult deleteCartaMano([FromRoute] string Id_Usuario, [FromRoute] string Id_Turno, [FromRoute] string Id_Carta)
        {
            try
            {
                CartasXTurnoXManoXUsuario deletedCartas = this.turnoData.deleteCartaMano(Id_Usuario, Id_Turno, Id_Carta);
                return Ok(deletedCartas);
            }
            catch (Exception e)
            {
                return BadRequest("");
            }
        }

        [HttpDelete]
        [Route("deleteCartaDeck/{Id_Usuario}/{Id_Turno}/{Id_Carta}")]
        public IActionResult deleteCartaDeck([FromRoute] string Id_Usuario, [FromRoute] string Id_Turno, [FromRoute] string Id_Carta)
        {
            try
            {    
                CartasXTurnoXDeckXUsuario deletedCartas = this.turnoData.deleteCartaDeck(Id_Usuario, Id_Turno, Id_Carta);
                return Ok(deletedCartas);
            }
            catch (Exception e)
            {
                return BadRequest("No se logró eliminar la carta del deck.");
            }
        }

        [HttpDelete]
        [Route("deleteCartaPlaneta/{Id_Usuario}/{Id_Turno}/{Id_Carta}")]
        public IActionResult deleteCartaPlaneta([FromRoute] string Id_Usuario, [FromRoute] string Id_Turno, [FromRoute] string Id_Carta)
        {
            try
            {   
                CartasXTurnoXPlanetaXUsuario deletedCartas = this.turnoData.deleteCartaPlaneta(Id_Usuario, Id_Turno, Id_Carta);
                return Ok(deletedCartas);
            }
            catch (Exception e)
            {
                return BadRequest("No se logró eliminar la carta.");
            }
        }

        [HttpPost]
        [Route("addTurno")]
        public IActionResult AddTurno(TurnoAPI turnoApi)
        {
            try
            {
                TurnoXUsuario turno = this.turnoData.addTurno(turnoApi);
                return Ok(turno);
            }
            catch (Exception e)
            {
                return BadRequest("No se logró añadir el turno.");
            }

        }

        [HttpGet]
        [Route("getLastTurno/{Id_Partida}/{Id_Usuario}")]
        public IActionResult getLastTurno([FromRoute] string Id_Partida, [FromRoute] string Id_Usuario)
        {
            try
            {   
                TurnoXUsuario turno = this.turnoData.getLastTurno(Id_Partida, Id_Usuario);
                return Ok(turno);
            }
            catch (Exception e)
            {
                return BadRequest("No se logró obtener el último turno.");
            }
        }

        [HttpGet]
        [Route("getLastTurnoNumero/{Id_Partida}/{Id_Usuario}/{Numero_Turno}")]
        public IActionResult getLastTurnoNumero([FromRoute] string Id_Partida, [FromRoute] string Id_Usuario, [FromRoute] int Numero_Turno)
        {
            try
            {
                TurnoXUsuario turno = this.turnoData.getLastTurnoNumero(Id_Partida, Id_Usuario, Numero_Turno);
                return Ok(turno);
            }
            catch (Exception e)
            {
                return BadRequest("No se logró obtener el turno " + Numero_Turno.ToString());
            }
        }

        [HttpGet]
        [Route("getTurno/{Id}")]
        public IActionResult getTurno([FromRoute] string Id)
        {
            try
            {
                return Ok(this.turnoData.getTurno(Id));
            }
            catch (Exception e)
            {
                return BadRequest("No se logró obtener el turno.");
            }
        }

        [HttpPut]
        [Route("updateTurno/{Id}")]
        public IActionResult updateTurno([FromRoute] string Id, TurnoAPI turnoApi)
        {
            try
            {
                TurnoXUsuario turno = this.turnoData.actualizarTurno(Id, turnoApi); 
                return Ok(turno);
            }
            catch (Exception e)
            {
                return BadRequest("No se logró actualizar el turno.");
            }
        }

        [HttpPut]
        [Route("updateEnergia/{Id}/{Id_Usuario}")]
        public IActionResult updateTurno([FromRoute] string Id, [FromRoute] string Id_Usuario,int energia)
        {
            try
            {
                TurnoXUsuario turno = this.turnoData.actualizarEnergia(Id, Id_Usuario, energia);
                return Ok(turno);
            }
            catch (Exception e)
            {
                return BadRequest("No se logró actualizar la energía.");
            }
        }

    }
}
