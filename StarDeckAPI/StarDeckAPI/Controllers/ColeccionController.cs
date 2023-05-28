using Microsoft.AspNetCore.Mvc;
using StarDeckAPI.Data;
using StarDeckAPI.Models;
using StarDeckAPI.Utilities;

namespace StarDeckAPI.Controllers
{
    [ApiController]
    [Route("colection")]
    public class ColeccionController : Controller
    {
        private APIDbContext apiDBContext;
        private ColeccionData coleccionData;

        public ColeccionController(APIDbContext apiDBContext)
        {
            this.apiDBContext = apiDBContext;
            this.coleccionData = new ColeccionData(apiDBContext);
        }

        [HttpGet]
        [Route("getcolection/{Id_usuario}")]
        public IActionResult GetColectionUser([FromRoute] string Id_usuario)
        {

            List<CartaAPI> cartasReturn = this.coleccionData.getColeccionDelUsuario(Id_usuario);

            return Ok(cartasReturn);
        }

        [HttpGet]
        [Route("getdecks/{Id_usuario}")]
        public IActionResult GetDecksUser([FromRoute] string Id_usuario)
        {
            
            List<DeckAPIGET> decksAPI = this.coleccionData.getDecksUsuario(Id_usuario);

            return Ok(decksAPI);
        }

        [HttpGet]
        [Route("getdeck/{Id}")]
        public IActionResult GetDeckUser([FromRoute] string Id)
        {

            DeckAPIGET element = this.coleccionData.getDeckUsuario(Id);
            return Ok(element);
        }

        /*
        [HttpGet]
        [Route("getUser")]
        public IActionResult GetUsers()
        {
            List<Usuario> users = apiDBContext.Usuario.ToList();
            return Ok(users);
        }
        */

        [HttpPost]
        [Route("addDeck")]
        public IActionResult AddDeck(DeckAPI deckApi)
        {

            Deck deck = this.coleccionData.addDeckUsuario(deckApi);
            return Ok(deck);

        }

        [HttpPost]
        [Route("addCartaUsuario")]
        public IActionResult AddCartaColeccion(CartaXUsuario cartaXUsuario)
        {

            CartaXUsuario cxu = this.coleccionData.addCartaColeccion(cartaXUsuario);

            return Ok(cxu);

        }

        [HttpPost]
        [Route("addCartaDeck")]
        public IActionResult AddCartaDeck(CartasXDeck cartasXDeck)
        {

            CartasXDeck cxd = this.coleccionData.AddCartaDeck(cartasXDeck);

            return Ok(cxd);

        }

        [HttpPut]
        [Route("update/{Id}")]
        public IActionResult UpdateDeck([FromRoute] string Id, Deck deckAPI)
        {
            Deck deckUser = this.coleccionData.actualizarDeck(Id, deckAPI);

            if (deckUser != null)
            {
                return Ok(deckUser);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("deleteDeck/{Id}")]
        public IActionResult DeleteDeck([FromRoute] string Id)
        {
            List<Deck> decks = apiDBContext.Deck.ToList();
            Deck deckUser = this.coleccionData.deleteDeck(Id);

            if (deckUser != null)
            {
                return Ok(deckUser);
            }

            return NotFound();

        }

        [HttpDelete]
        [Route("deleteCartaDeck/{Id_Deck}/{Id_Carta}")]
        public IActionResult DeleteCartaDeck([FromRoute] string Id_Deck, string Id_Carta)
        {
            CartasXDeck cxdFiltered = this.coleccionData.deleteCartaDeck(Id_Deck, Id_Carta);

            if (cxdFiltered != null)
            {
                return Ok(cxdFiltered);
            }

            return NotFound();

        }

    }
}
