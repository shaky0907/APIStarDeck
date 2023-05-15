using Microsoft.AspNetCore.Mvc;
using StarDeckAPI.Data;
using StarDeckAPI.Models;
using StarDeckAPI.Utilities;
using System.Collections.Generic;

namespace StarDeckAPI.Controllers
{
    [ApiController]
    [Route("colection")]
    public class ColeccionController : Controller
    {
        private readonly APIDbContext apiDBContext;

        public ColeccionController(APIDbContext apiDBContext)
        {
            this.apiDBContext = apiDBContext;
        }

        [HttpGet]
        [Route("getcolection/{Id_usuario}")]
        public IActionResult GetColectionUser([FromRoute] string Id_usuario)
        {
            List<CartaXUsuario> colection = apiDBContext.CartaXUsuario.ToList();
            List<CartaXUsuario> colectionUser = colection.Where(x => x.Id_usuario == Id_usuario).ToList();
            return Ok(colectionUser);
        }

        [HttpGet]
        [Route("getdecks/{Id_usuario}")]
        public IActionResult GetDecksUser([FromRoute] string Id_usuario)
        {
            List<Deck> decks = apiDBContext.Deck.ToList();
            List<Deck> decksUser = decks.Where(x => x.Id_usuario == Id_usuario).ToList();
            /*
            List<string> cartasIds = new List<string>();
            foreach (Deck deck in decksUser)
            {
                cartasIds.Add(deck.Id);
            }
            List<CartasXDeck> cartasXDeck = apiDBContext.CartasXDeck.ToList().Where(x => cartasIds.Contains(x.Id_Deck)).ToList();
            */
            List<CartasXDeck> cartasXDeck = apiDBContext.CartasXDeck.ToList();
            List<DeckAPIGET> deckAPI = new List<DeckAPIGET>();

            foreach (Deck deck in decksUser)
            {
                DeckAPIGET element = new DeckAPIGET()
                {
                    Id = deck.Id,
                    Nombre = deck.Nombre,
                    Estado = deck.Estado,
                    Slot = deck.Slot,
                    Id_usuario = deck.Id,
                    id_cartas = cartasXDeck.Where(x => x.Id_Deck == deck.Id).Select(x => x.Id_Carta).ToList()


                };
                deckAPI.Add(element);
            }

            return Ok(deckAPI);
        }

        [HttpGet]
        [Route("getdeck/{Id}")]
        public IActionResult GetDeckUser([FromRoute] string Id)
        {
            List<Deck> decks = apiDBContext.Deck.ToList();
            Deck deckUser = decks.Where(x => x.Id == Id).First();

            List<CartasXDeck> cartasXDeck = apiDBContext.CartasXDeck.ToList();

            DeckAPIGET element = new DeckAPIGET()
            {
                Id = deckUser.Id,
                Nombre = deckUser.Nombre,
                Estado = deckUser.Estado,
                Slot = deckUser.Slot,
                Id_usuario = deckUser.Id,
                id_cartas = cartasXDeck.Where(x => x.Id_Deck == deckUser.Id).Select(x => x.Id_Carta).ToList()


            };
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
            string id = GeneratorID.GenerateRandomId("D-");
            Deck deck = new Deck()
            {
                Id = id,
                Nombre = deckApi.Nombre,
                Estado = deckApi.Estado,
                Slot = deckApi.Slot,
                Id_usuario = deckApi.Id_usuario
            };
            apiDBContext.Deck.Add(deck);
            apiDBContext.SaveChanges();

            foreach (string Id_Carta in deckApi.id_cartas)
            {
                CartasXDeck cxd = new CartasXDeck()
                {
                    Id_Carta = Id_Carta,
                    Id_Deck = id
                };
                apiDBContext.CartasXDeck.Add( cxd);

            }
            
            apiDBContext.SaveChanges();

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
                deckUser.Id = Id;
                deckUser.Nombre = deckAPI.Nombre;
                deckUser.Estado = deckAPI.Estado;
                deckUser.Id_usuario = deckAPI.Id_usuario;
                deckUser.Slot = deckAPI.Slot;

                apiDBContext.Update(deckUser);
                /*
                foreach (string Id_Carta in deckAPI.id_cartas)
                {
                    CartasXDeck cxd = new CartasXDeck()
                    {
                        Id_Carta = Id_Carta,
                        Id_Deck = Id
                    };
                    apiDBContext.CartasXDeck.Update(cxd);
                }
                */
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
