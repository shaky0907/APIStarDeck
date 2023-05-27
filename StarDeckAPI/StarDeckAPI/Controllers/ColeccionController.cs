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

            CartaXUsuario cxu = new CartaXUsuario()
            {
                Id_usuario = cartaXUsuario.Id_usuario,
                Id_carta = cartaXUsuario.Id_carta
            };
            apiDBContext.CartaXUsuario.Add(cxu);

            apiDBContext.SaveChanges();

            return Ok(cxu);

        }

        [HttpPost]
        [Route("addCartaDeck")]
        public IActionResult AddCartaColeccion(CartasXDeck cartasXDeck)
        {

            CartasXDeck cxd = new CartasXDeck()
            {
                Id_Carta = cartasXDeck.Id_Carta,
                Id_Deck = cartasXDeck.Id_Deck
            };
            apiDBContext.CartasXDeck.Add(cxd);

            apiDBContext.SaveChanges();

            return Ok(cxd);

        }

        [HttpPut]
        [Route("update/{Id}")]
        public IActionResult UpdateDeck([FromRoute] string Id, Deck deckAPI)
        {
            List<Deck> decks = apiDBContext.Deck.ToList();
            Deck deckUser = decks.Where(x => x.Id == Id).First();

            if (deckUser != null)
            {
                deckUser.Estado = deckAPI.Estado;
                
                apiDBContext.Update(deckUser);
                apiDBContext.SaveChanges();

                return Ok(deckUser);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("deleteDeck/{Id}")]
        public IActionResult DeleteDeck([FromRoute] string Id)
        {
            List<Deck> decks = apiDBContext.Deck.ToList();
            Deck deckUser = decks.Where(x => x.Id == Id).First();

            List<CartasXDeck> cxdL = apiDBContext.CartasXDeck.ToList().Where(x => x.Id_Deck == Id).ToList();

            if (cxdL.Any())
            {
                foreach (CartasXDeck cxd in cxdL)
                {
                    apiDBContext.Remove(cxd);
                }
            }
            apiDBContext.SaveChanges();

            if (deckUser != null)
            {
                apiDBContext.Remove(deckUser);
                apiDBContext.SaveChanges();
                return Ok(deckUser);
            }

            return NotFound();

        }

        [HttpDelete]
        [Route("deleteCartaDeck/{Id_Deck}/{Id_Carta}")]
        public IActionResult DeleteCartaDeck([FromRoute] string Id_Deck, string Id_Carta)
        {
            List<CartasXDeck> cxdL = apiDBContext.CartasXDeck.ToList();
            CartasXDeck cxdFiltered = cxdL.Where(x => x.Id_Deck == Id_Deck).Where(x => x.Id_Carta == Id_Carta).First();

            if (cxdFiltered != null)
            {
                apiDBContext.Remove(cxdFiltered);
                apiDBContext.SaveChanges();
                return Ok(cxdFiltered);
            }

            return NotFound();

        }

    }
}
