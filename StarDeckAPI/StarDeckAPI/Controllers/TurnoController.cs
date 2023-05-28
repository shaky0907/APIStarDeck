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
        private TurnoData TurnoData;
        public TurnoController(APIDbContext apiDBContext)
        {
            this.apiDBContext = apiDBContext;
            this.turnoData = new TurnoData(apiDBContext);
        }

        [HttpGet]
        [Route("getmano/{Id_usuario}/{Id_turno}")]
        public IActionResult GetUserMano([FromRoute] string Id_usuario, [FromRoute] string Id_turno)
        {

            List<string> Cartas = this.apiDBContext.CartasXTurnoXManoXUsuario.ToList().Where(x => (x.Id_Turno == Id_turno) && (x.Id_Usuario == Id_usuario)).ToList();

            return Ok(Cartas);
        }

        [HttpGet]
        [Route("getdeck/{Id_usuario}/{Id_turno}")]
        public IActionResult GetUserDeck([FromRoute] string Id_usuario, [FromRoute] string Id_turno)
        {

            List<string> Cartas = this.apiDBContext.CartasXTurnoXDeckXUsuario.ToList().Where(x => (x.Id_Turno == Id_turno) && (x.Id_Usuario == Id_usuario)).ToList();

            return Ok(Cartas);
        }


        [HttpPost]
        [Route("addCartaTurnoManoUsuario")]
        public IActionResult AddCartaMano(CartasXTurnoXManoXUsuario cartasxturnoxmanoxusuario)
        {

            CartasXTurnoXManoXUsuario cxtxmxu = new CartasXTurnoXManoXUsuario()
            {
                Id_Turno = cartasxturnoxmanoxusuario.Id_Turno,
                Id_Carta = cartasxturnoxmanoxusuario.Id_Carta,
                Id_Usuario = cartasxturnoxmanoxusuario.Id_Usuario
            };
            apiDBContext.CartasXTurnoXDeckXUsuario.Add(cxtxmxu);

            apiDBContext.SaveChanges();

            return Ok(cxtxmxu);

        }

        [HttpPost]
        [Route("addCartaTurnoDeckUsuario")]
        public IActionResult AddCartaDeck(CartasXTurnoXDeckXUsuario cartasxturnoxdeckxusuario)
        {

            CartasXTurnoXDeckXUsuario cxtxdxu = new CartasXTurnoXDeckXUsuario()
            {
                Id_Carta = cartasxturnoxdeckxusuario.Id_Carta,
                Id_Turno = cartasxturnoxdeckxusuario.Id_Turno,
                Id_Usuario = cartasxturnoxdeckxusuario.Id_Usuario
            };
            apiDBContext.CartasXTurnoXDeckXUsuario.Add(cartasxturnoxdeckxusuario);

            apiDBContext.SaveChanges();

            return Ok(cartasxturnoxdeckxusuario);

        }



    }
}
