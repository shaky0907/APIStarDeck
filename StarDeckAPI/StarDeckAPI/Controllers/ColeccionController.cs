﻿using Microsoft.AspNetCore.JsonPatch.Internal;
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

            List<Carta> cartas = apiDBContext.Carta.ToList();
            List<Carta> cartasUser = new List<Carta>();

            foreach (CartaXUsuario carta in colectionUser)
            {
                Carta cartaUser = cartas.Where(x => x.Id == carta.Id_carta).First();
                cartasUser.Add(cartaUser);
            }
            

            List<CartaAPI> cartasReturn = new List<CartaAPI>();

            foreach (Carta carta in cartasUser)
            {
                CartaAPI cApi = new CartaAPI()
                {
                    Id = carta.Id,
                    Nombre = carta.N_Personaje,
                    Energia = carta.Energia,
                    Costo = carta.C_batalla,
                    Imagen = carta.Imagen,
                    Raza = apiDBContext.Raza.ToList().Where(x => x.Id == carta.Raza).First().Nombre,
                    Tipo = apiDBContext.Tipo.ToList().Where(x => x.Id == carta.Tipo).First().Nombre,
                    Estado = carta.Activa,
                    Descripcion = carta.Descripcion
                };

                cartasReturn.Add(cApi);
            }

            return Ok(cartasReturn);
        }

        [HttpGet]
        [Route("getdecks/{Id_usuario}")]
        public IActionResult GetDecksUser([FromRoute] string Id_usuario)
        {
            List<Deck> decks = apiDBContext.Deck.ToList();
            List<Deck> decksUser = decks.Where(x => x.Id_usuario == Id_usuario).ToList();

            List<CartasXDeck> cartasXDeck = apiDBContext.CartasXDeck.ToList();
            List<DeckAPIGET> deckAPI = new List<DeckAPIGET>();

            CartaController cartas = new CartaController(apiDBContext);

            

            foreach (Deck deck in decksUser)
            {
                List<CartaAPI> Cartas = new List<CartaAPI>();
                List<String> ids = cartasXDeck.Where(x => x.Id_Deck == deck.Id).Select(x => x.Id_Carta).ToList();

                foreach (String id in ids)
                {
                    Carta carta = apiDBContext.Carta.ToList().Where(x => x.Id == id).First();
                    CartaAPI cApi = new CartaAPI()
                    {
                        Id = carta.Id,
                        Nombre = carta.N_Personaje,
                        Energia = carta.Energia,
                        Costo = carta.C_batalla,
                        Imagen = carta.Imagen,
                        Raza = apiDBContext.Raza.ToList().Where(x => x.Id == carta.Raza).First().Nombre,
                        Tipo = apiDBContext.Tipo.ToList().Where(x => x.Id == carta.Tipo).First().Nombre,
                        Estado = carta.Activa,
                        Descripcion = carta.Descripcion
                    };
                    Cartas.Add(cApi);
                }


                DeckAPIGET element = new DeckAPIGET()
                {
                    Id = deck.Id,
                    Nombre = deck.Nombre,
                    Estado = deck.Estado,
                    Id_usuario = deck.Id,
                    Cartas = Cartas


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
                Id_usuario = deckUser.Id,
                //id_cartas = cartasXDeck.Where(x => x.Id_Deck == deckUser.Id).Select(x => x.Id_Carta).ToList()


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
                Id_usuario = deckApi.Id_usuario
            };
            apiDBContext.Deck.Add(deck);
            apiDBContext.SaveChanges();

            foreach (CartaAPI carta in deckApi.Cartas)
            {
                CartasXDeck cxd = new CartasXDeck()
                {
                    Id_Carta = carta.Id,
                    Id_Deck = id
                };
                apiDBContext.CartasXDeck.Add(cxd);

            }
            
            apiDBContext.SaveChanges();

            List<Deck> decks = apiDBContext.Deck.ToList();
            List<Deck> decksToUpdate = decks.Where(x => x.Id != id).ToList();

            foreach (Deck deckToUpdate in decksToUpdate)
            {
                //UpdateDeck(deckToUpdate.Id, deckToUpdate);
                deckToUpdate.Estado = false;

                apiDBContext.Update(deckToUpdate);

                apiDBContext.SaveChanges();
            }

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
