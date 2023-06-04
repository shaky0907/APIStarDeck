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
        private APIDbContext apiDBContext;
        private CartaData cartaData;


        public CartaController(APIDbContext apiDBContext)
        {
            this.apiDBContext = apiDBContext;
            this.cartaData = new CartaData(apiDBContext);
        }

        [HttpGet]
        [Route("razas")]
        public IActionResult Razas()
        {
            List<Raza> razas = apiDBContext.Raza.ToList();

            return Ok(razas);
        }

        

        [HttpGet]
        [Route("getCartasTest")]
        public IActionResult testFunc()
        {
            List<Carta> cartas = apiDBContext.Carta.ToList();
            return Ok(cartas);

        }


        [HttpGet]
        [Route("getnewDeck")]
        public IActionResult GetNewDeck()
        {

            List<CartaAPI> cartasReturn = this.cartaData.getCartasNuevoDeck();
            return Ok(cartasReturn);

        }

        [HttpGet]
        [Route("lista")]
        public IActionResult GetAll()
        {
            List<CartaAPI> cartasReturn = this.cartaData.getAllCartas();

            return Ok(cartasReturn);
        }
        
        [HttpGet]
        [Route("lista/{Id}")]
        public IActionResult Get([FromRoute] string Id)
        {
            CartaAPI cApi = this.cartaData.getCartaDB(Id);

            return Ok(cApi);
        }

        [HttpPost]
        [Route("guardar")]
        public IActionResult saveCarta(CartaAPI cartaAPI)
        {
            Carta cartaReturn = this.cartaData.guardarCartaDB(cartaAPI);

            return Ok(cartaReturn);
        }

        [HttpDelete]
        [Route("delete/{Id}")]
        public IActionResult deleteCarta([FromRoute] string Id)
        {
            Carta carta = this.cartaData.deleteCartaDB(Id);

            return Ok(carta);
        }
        
    }
}
