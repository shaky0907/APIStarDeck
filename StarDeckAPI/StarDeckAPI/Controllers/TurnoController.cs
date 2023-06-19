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
        private readonly ILogger<CartaController> _logger;

        public TurnoController(APIDbContext apiDBContext, ILogger<CartaController> logger)
        {
            this.apiDBContext = apiDBContext;
            this.turnoData = new TurnoData(apiDBContext);
            _logger = logger;
        }

        [HttpGet]
        [Route("getmano/{Id_usuario}/{Id_turno}")]
        public IActionResult GetUserMano([FromRoute] string Id_usuario, [FromRoute] string Id_turno)
        {
            try
            {
                List<CartaAPI> cartas_Ids = this.turnoData.getUserMano(Id_usuario, Id_turno);
                _logger.LogInformation("Se envio la informacion de la mano del usuario correctamente");
                return Ok(cartas_Ids);
            }
            catch (Exception e)
            {
                _logger.LogError("No se logro obtener la mano del usuario: " + Id_usuario);
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
                _logger.LogInformation("Se envio la informacion del deck correctamente");
                return Ok(cartas_Ids);
            }
            catch (Exception e)
            {
                _logger.LogError("No se logro obtener el deck del usuario en el turno solicitado.");
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
                _logger.LogInformation("Se envio la informacion de las cartas de cada planeta correctamente");
                return Ok(cartas_Ids);
            }
            catch (Exception e)
            {
                _logger.LogError("No se logró obtener las cartas del planeta solicitado");
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
                _logger.LogInformation("Se envio la informacion de las cartas de cada planeta correctamente");
                return Ok(cartas_Ids);
            }
            catch (Exception e)
            {
                _logger.LogError($"{e.Message}");
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
                _logger.LogInformation("Se envio la informacion del ganador de cada planeta correctamente");
                return Ok(ganador);
            }
            catch (Exception e)
            {
                _logger.LogError("No se logró obtener el ganador de la partida solicitada");
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
                _logger.LogInformation("Se envio la informacion del turno correctamente");
                return Ok(turnoCompleto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
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
                _logger.LogInformation("Se actualizo la informacion del turno correctamente");
                return Ok(turnoCompletoUpdated);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
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
                _logger.LogInformation("Se rindio de la partida correctamente");
                return Ok(usuarioXPartida);
            }
            catch (Exception e)
            {
                _logger.LogError("No se logró rendirse de la partida.");
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
                _logger.LogInformation("Se creo el turno nuevo de la partida correctamente");
                return Ok(turnoCompletoNuevo);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
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
                _logger.LogInformation("Se creo el turno nuevo de la partida correctamente");
                return Ok(turnoCompletoNuevo);
            }
            catch (Exception e)
            {
                _logger.LogError("No se logró crear el turno incial");
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
                _logger.LogInformation("Se anadio la carta en la mano correctamente");
                return Ok(cxtxmxu);
            }
            catch (Exception e)
            {
                _logger.LogError($"{e.Message}", e);
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
                _logger.LogInformation("Se anadio la carta al deck de la partida correctamente");
                return Ok(cartasxturnoxdeckxusuario);
            }
            catch (Exception e)
            {
                _logger.LogError($"{e.Message}", e);
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
                _logger.LogInformation("Se anadio la carta al planeta correctamente");
                return Ok(cartasXTurnoXPlanetaXUsuario);
            }
            catch (Exception e)
            {
                _logger.LogError($"{e.Message}", e);
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
                _logger.LogInformation("Se borro la carta en la mano correctamente");
                return Ok(deletedCartas);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString(), e);
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
                _logger.LogInformation("Se borro la carta del deck correctamente");
                return Ok(deletedCartas);
            }
            catch (Exception e)
            {
                _logger.LogError("No se logró eliminar la carta del deck.");
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
                _logger.LogInformation("Se borro la carta del planeta correctamente");
                return Ok(deletedCartas);
            }
            catch (Exception e)
            {
                _logger.LogError($"{e.Message}");
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
                _logger.LogInformation("Se anadio el turno correctamente");
                return Ok(turno);
            }
            catch (Exception e)
            {
                _logger.LogError("No se logró añadir el turno.");
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
                _logger.LogInformation("Se envio la informacion del ultimo turno correctamente");
                return Ok(turno);
            }
            catch (Exception e)
            {
                _logger.LogError("No se logró obtener el último turno");
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
                _logger.LogInformation("Se envio la informacion del ultimo turno correctamente");
                return Ok(turno);
            }
            catch (Exception e)
            {
                _logger.LogError("No se logró obtener el turno " + Numero_Turno.ToString());
                return BadRequest("No se logró obtener el turno " + Numero_Turno.ToString());
            }
        }

        [HttpGet]
        [Route("getTurno/{Id}")]
        public IActionResult getTurno([FromRoute] string Id)
        {
            try
            {
                _logger.LogInformation("Se envio la informacion del turno correctamente");
                return Ok(this.turnoData.getTurno(Id));
            }
            catch (Exception e)
            {
                _logger.LogError("No se logró obtener el turno");
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
                _logger.LogInformation("Se actualizo el turno correctamente");
                return Ok(turno);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
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
                _logger.LogInformation("Se actualizo la energia correctamente");
                return Ok(turno);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest("No se logró actualizar la energía.");
            }
        }

    }
}
