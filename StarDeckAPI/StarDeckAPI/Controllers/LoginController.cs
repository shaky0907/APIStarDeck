using Microsoft.AspNetCore.Mvc;
using StarDeckAPI.Data;
using StarDeckAPI.Models;

namespace StarDeckAPI.Controllers
{
    [Route("login")]
    [ApiController]
    public class LoginController : Controller
    {

        private readonly APIDbContext apiDBContext;
        private readonly ILogger<CartaController> _logger;

        public LoginController(APIDbContext apiDBContext, ILogger<CartaController> logger)
        {
            this.apiDBContext = apiDBContext;
            _logger = logger;
        }
        [HttpPost]
        [Route("login")]
        public IActionResult LoginCheck(Login login)
        {
            List<Usuario> usuarios = apiDBContext.Usuario.ToList().Where(x => (x.Correo == login.Correo) 
            && (x.Contrasena == login.Contrasena) ).ToList();

            

            if (usuarios.Any())
            {
                Usuario usuario = usuarios.First();
                LoginResponse responseOk = new LoginResponse()
                {
                    found = true,
                    usuario = usuario
                };

                return Ok(responseOk);
            }

            LoginResponse response = new LoginResponse()
            {
                found = false,
                usuario = new Usuario()
            };

            _logger.LogInformation("Se envio la informacion del login correctamente");
            return Ok(response);
            
        }
    }
}
