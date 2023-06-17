using Microsoft.AspNetCore.Mvc;
using StarDeckAPI.Controllers;
using StarDeckAPI.Models;
using StarDeckAPI.Utilities;

namespace StarDeckAPI.Data
{
    public class ColeccionData
    {
        private APIDbContext apiDBContext;

        public ColeccionData(APIDbContext apiDBContext)
        {
            this.apiDBContext = apiDBContext;
        }

        public List<CartaAPI> getColeccionDelUsuario(string Id_usuario)
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

            return cartasReturn;
        }

        public List<DeckAPIGET> getDecksUsuario(string Id_usuario)
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

            return deckAPI;
        }

        public DeckAPIGET getDeckUsuario(string Id)
        {
            List<Deck> decks = apiDBContext.Deck.ToList();
            Deck deckUser = decks.Where(x => x.Id == Id).First();

            List<CartasXDeck> cartasXDeck = apiDBContext.CartasXDeck.ToList();

            DeckAPIGET element = new DeckAPIGET()
            {
                Id = deckUser.Id,
                Nombre = deckUser.Nombre,
                Estado = deckUser.Estado,
                Id_usuario = deckUser.Id
                //id_cartas = cartasXDeck.Where(x => x.Id_Deck == deckUser.Id).Select(x => x.Id_Carta).ToList()
            };
            return element;
        }

        public Deck addDeckUsuario(DeckAPI deckApi)
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
            List<Deck> decksToUpdate = decks.Where(x => x.Id != id && x.Id_usuario == deckApi.Id_usuario).ToList();

            foreach (Deck deckToUpdate in decksToUpdate)
            {
                deckToUpdate.Estado = false;

                apiDBContext.Update(deckToUpdate);
            }

            apiDBContext.SaveChanges();
            return deck;
        }

        public CartaXUsuario addCartaColeccion(CartaXUsuario cartaXUsuario)
        {

            CartaXUsuario cxu = new CartaXUsuario()
            {
                Id_usuario = cartaXUsuario.Id_usuario,
                Id_carta = cartaXUsuario.Id_carta
            };
            apiDBContext.CartaXUsuario.Add(cxu);

            apiDBContext.SaveChanges();

            return cxu;
        }

        public CartasXDeck AddCartaDeck(CartasXDeck cartasXDeck)
        {

            CartasXDeck cxd = new CartasXDeck()
            {
                Id_Carta = cartasXDeck.Id_Carta,
                Id_Deck = cartasXDeck.Id_Deck
            };
            apiDBContext.CartasXDeck.Add(cxd);

            apiDBContext.SaveChanges();

            return cxd;
        }

        public Deck actualizarDeck(string Id, Deck deckAPI)
        {
            List<Deck> decks = apiDBContext.Deck.ToList();
            Deck deckUser = decks.Where(x => x.Id == Id).First();

            if (deckUser != null)
            {
                deckUser.Estado = deckAPI.Estado;

                apiDBContext.Update(deckUser);
                apiDBContext.SaveChanges();

               
            }
            return deckUser;
        }

        public Deck deleteDeck(string Id)
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
                
            }

            return deckUser;

        }

        public CartasXDeck deleteCartaDeck(string Id_Deck, string Id_Carta)
        {
            List<CartasXDeck> cxdL = apiDBContext.CartasXDeck.ToList();
            CartasXDeck cxdFiltered = cxdL.Where(x => x.Id_Deck == Id_Deck).Where(x => x.Id_Carta == Id_Carta).First();

            if (cxdFiltered != null)
            {
                apiDBContext.Remove(cxdFiltered);
                apiDBContext.SaveChanges();
                
            }

            return cxdFiltered;

        }
    }

    
}
