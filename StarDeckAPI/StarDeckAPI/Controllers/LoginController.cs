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

        public LoginController(APIDbContext apiDBContext)
        {
            this.apiDBContext = apiDBContext;
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

            return Ok(response);
            
        }
    }
}
