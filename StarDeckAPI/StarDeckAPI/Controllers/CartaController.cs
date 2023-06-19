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

        private readonly ILogger<CartaController> _logger;

        public CartaController(APIDbContext apiDBContext, ILogger<CartaController> logger)
        {
            this.apiDBContext = apiDBContext;
            this.cartaData = new CartaData(apiDBContext);
            _logger = logger;
        }

        [HttpGet]
        [Route("razas")]
        public IActionResult Razas()
        {
            List<Raza> razas = apiDBContext.Raza.ToList();
            _logger.LogInformation("Se envio la informacion de las razas correctamente");
            return Ok(razas);
        }


        [HttpGet]
        [Route("getnewDeck")]
        public IActionResult GetNewDeck()
        {
            List<CartaAPI> cartasReturn = this.cartaData.getCartasNuevoDeck();
            _logger.LogInformation("Se envio la informacion del deck correctamente");
            return Ok(cartasReturn);
        }

        [HttpGet]
        [Route("lista")]
        public IActionResult GetAll()
        {
            List<CartaAPI> cartasReturn = this.cartaData.getAllCartas();
            _logger.LogInformation("Se envio la informacion de las cartas correctamente");
            return Ok(cartasReturn);
        }
        
        [HttpGet]
        [Route("lista/{Id}")]
        public IActionResult Get([FromRoute] string Id)
        {
            try
            {
                CartaAPI cApi = this.cartaData.getCartaDB(Id);
                _logger.LogInformation("Se envio la informacion de la carta correctamente");
                return Ok(cApi);
            } 
            catch (Exception e)
            {
                _logger.LogError("La carta " + Id + " no fue encontrada");
                return BadRequest("No se logró encontrar la carta solicitada.");
            }
        }

        [HttpPost]
        [Route("guardar")]
        public IActionResult saveCarta(CartaAPI cartaAPI)
        {
            Carta cartaReturn = this.cartaData.guardarCartaDB(cartaAPI);
            _logger.LogInformation("La carta fue guardada correctamente");
            return Ok(cartaReturn);
        }

        [HttpDelete]
        [Route("delete/{Id}")]
        public IActionResult deleteCarta([FromRoute] string Id)
        {
            try
            {
                Carta carta = this.cartaData.deleteCartaDB(Id);
                _logger.LogInformation("La carta fue eliminada correctamente");

                return Ok(carta);
            }
            catch (Exception e)
            {
                _logger.LogInformation("La carta" + Id +" no existe en la base de datos");
                return BadRequest("No se pudo eliminar la carta.");
            }
        }
        
    }
}
