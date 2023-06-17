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
            try 
            {
                List<CartaAPI> cartasReturn = this.coleccionData.getColeccionDelUsuario(Id_usuario);
                return Ok(cartasReturn);
            }
            catch (Exception e)
            {
                return BadRequest("No se logró obtener el usuario solicitado.");
            }
        }

        [HttpGet]
        [Route("getdecks/{Id_usuario}")]
        public IActionResult GetDecksUser([FromRoute] string Id_usuario)
        {
            try
            {
                List<DeckAPIGET> decksAPI = this.coleccionData.getDecksUsuario(Id_usuario);
                return Ok(decksAPI);
            }
            catch (Exception e)
            {
                return BadRequest("No se logró obtener la lista de decks.");
            }
        }

        [HttpGet]
        [Route("getdeck/{Id}")]
        public IActionResult GetDeckUser([FromRoute] string Id)
        {
            try
            {
                DeckAPIGET element = this.coleccionData.getDeckUsuario(Id);
                return Ok(element);
            }
            catch (Exception e)
            {
                return BadRequest("No se logró obtener la información del Deck.");
            }
        }

        [HttpPost]
        [Route("addDeck")]
        public IActionResult AddDeck(DeckAPI deckApi)
        {
            try
            {
                Deck deck = this.coleccionData.addDeckUsuario(deckApi);
                return Ok(deck);
            }
            catch (Exception e)
            {
                return BadRequest("No se logró añadir el Deck.");
            }
        }

        [HttpPost]
        [Route("addCartaUsuario")]
        public IActionResult AddCartaColeccion(CartaXUsuario cartaXUsuario)
        {
            try
            {
                CartaXUsuario cxu = this.coleccionData.addCartaColeccion(cartaXUsuario);
                return Ok(cxu);
            }
            catch (Exception e)
            {
                return BadRequest("No se logró añadir la carta a la colección del usuario.");
            }
        }

        [HttpPost]
        [Route("addCartaDeck")]
        public IActionResult AddCartaDeck(CartasXDeck cartasXDeck)
        {
            try
            {
                CartasXDeck cxd = this.coleccionData.AddCartaDeck(cartasXDeck);
                return Ok(cxd);
            }
            catch (Exception e)
            {
                return BadRequest("No se logró añadir la carta al deck.");
            }
        }

        [HttpPut]
        [Route("update/{Id}")]
        public IActionResult UpdateDeck([FromRoute] string Id, Deck deckAPI)
        {
            try
            {
                Deck deckUser = this.coleccionData.actualizarDeck(Id, deckAPI);
                return Ok(deckUser);
            }
            catch (Exception e)
            {
                return BadRequest("No se logró actualizar el deck solicitado.");
            }
        }

        [HttpDelete]
        [Route("deleteDeck/{Id}")]
        public IActionResult DeleteDeck([FromRoute] string Id)
        {
            try
            {
                List<Deck> decks = apiDBContext.Deck.ToList();
                Deck deckUser = this.coleccionData.deleteDeck(Id);
                return Ok(deckUser);
            }
            catch (Exception e)
            {
                return BadRequest("No se logró eliminar el Deck.");            
            }

        }

        [HttpDelete]
        [Route("deleteCartaDeck/{Id_Deck}/{Id_Carta}")]
        public IActionResult DeleteCartaDeck([FromRoute] string Id_Deck, string Id_Carta)
        {
            try
            {
                CartasXDeck cxdFiltered = this.coleccionData.deleteCartaDeck(Id_Deck, Id_Carta);
                return Ok(cxdFiltered);
            }
            catch (Exception e)
            {
                return BadRequest("No se logró eliminar la carta del deck.");
            }

        }

    }
}
