using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StarDeckAPI.Data;
using StarDeckAPI.Models;
using StarDeckAPI.Utilities;
using System;

namespace StarDeckAPI.Controllers
{   
    [ApiController]
    [Route("carta")]

    public class CartaController : Controller
    {
        private readonly APIDbContext apiDBContext;
        private Random random = new Random();

        public CartaController(APIDbContext apiDBContext)
        {
            this.apiDBContext = apiDBContext;
        }

        [HttpGet]
        [Route("razas")]
        public IActionResult Razas()
        {
            List<Raza> razas = apiDBContext.Raza.ToList();

            return Ok(razas);
        }

        private List<Carta> GenerateRandomCartas(List<Carta> inputCards, int count)
        {
            if (inputCards.Count() < count)
            {
                throw new ArgumentException("The input list must contain at least 15 cards.");
            }

            List<Carta> selectedCards = new List<Carta>();

            while (selectedCards.Count() < count)
            {
                int randomIndex = random.Next(inputCards.Count());
                Carta randomCard = inputCards[randomIndex];

                if (!selectedCards.Contains(randomCard))
                {
                    selectedCards.Add(randomCard);
                }
            }

            return selectedCards;
        }

        [HttpGet]
        [Route("getnewDeck")]
        public IActionResult GetNewDeck()
        {
            List<Carta> cartasBasicas = apiDBContext.Carta.ToList().Where(x => x.Tipo == 5).ToList();

            List<Carta> cartasTotales = GenerateRandomCartas(cartasBasicas, 15);

            List<Carta> cartasrn = apiDBContext.Carta.ToList().Where(x => (x.Tipo == 4) || (x.Tipo == 3)).ToList();

            List<Carta> cartasrestantes = GenerateRandomCartas(cartasrn, 9);

            cartasTotales.AddRange(cartasrestantes);

            List<CartaAPI> cartasReturn = new List<CartaAPI>();

            foreach (Carta carta in cartasTotales)
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
<<<<<<< HEAD

                cartasReturn.Add(cApi);
=======
>>>>>>> ee2203c1d2b088f89d16eff8400ba1cc64413834
            }

            return Ok(cartasReturn);

        }

        [HttpGet]
        [Route("lista")]
        public IActionResult GetAll()
        {
            List<Carta> cartas = apiDBContext.Carta.ToList();
            List<CartaAPI> cartasReturn = new List<CartaAPI>();

            foreach (Carta carta in cartas)
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
<<<<<<< HEAD
                cartasReturn.Add(cApi);
=======
>>>>>>> ee2203c1d2b088f89d16eff8400ba1cc64413834
            }

            return Ok(cartasReturn);
        }
        
        [HttpGet]
        [Route("lista/{Id}")]
        public IActionResult Get([FromRoute] string Id)
        {
            Carta carta = apiDBContext.Carta.ToList().Where(x => x.Id == Id).First();
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

            return Ok(cApi);
        }

        [HttpPost]
        [Route("guardar")]
        public IActionResult saveCarta(CartaAPI cartaAPI)
        {
            Carta carta = new Carta()
            {
                Id = GeneratorID.GenerateRandomId("C-"),
                N_Personaje = cartaAPI.Nombre,
                Energia = cartaAPI.Energia,
                C_batalla = cartaAPI.Costo,
                Imagen = cartaAPI.Imagen,
                Raza = apiDBContext.Raza.ToList().Where(x => x.Nombre == cartaAPI.Raza).First().Id,
                Tipo = apiDBContext.Tipo.ToList().Where(x => x.Nombre == cartaAPI.Tipo).First().Id,
                Activa = cartaAPI.Estado,
                Descripcion = cartaAPI.Descripcion
            };
            apiDBContext.Add(carta);
            apiDBContext.SaveChanges();

            return Ok(cartaAPI);
        }

        [HttpDelete]
        [Route("delete/{Id}")]
        public IActionResult deleteCarta([FromRoute] string Id)
        {
            Carta carta = apiDBContext.Carta.ToList().Where(x => x.Id == Id).First();
            apiDBContext.Remove(carta);
            apiDBContext.SaveChanges();
            return Ok(carta);
        }
        
    }
}
