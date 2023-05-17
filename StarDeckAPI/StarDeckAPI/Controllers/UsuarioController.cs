using Microsoft.AspNetCore.Mvc;
using StarDeckAPI.Data;
using StarDeckAPI.Models;
using StarDeckAPI.Utilities;
using System.Numerics;
using System.Text.RegularExpressions;

namespace StarDeckAPI.Controllers
{
    [ApiController]
    [Route("usuario")]

    public class UsuarioController : Controller
    {
        private readonly APIDbContext apiDBContext;
        private Random random = new Random();

        public UsuarioController(APIDbContext apiDBContext)
        {
            this.apiDBContext = apiDBContext;
        }

        [HttpGet]
        [Route("lista")]
        public IActionResult GetAll()
        {
            List<Usuario> list_usuario = apiDBContext.Usuario.ToList();
            List<UsuarioAPI> list_return = new List<UsuarioAPI>();
            foreach (Usuario usuario in list_usuario)
            {
                UsuarioAPI uApi = new UsuarioAPI()
                {
                    Id = usuario.Id,
                    Administrador = usuario.Administrador,
                    Nombre = usuario.Nombre,
                    Username = usuario.Username,
                    Contrasena = usuario.Contrasena,
                    Correo = usuario.Correo,
                    Nacionalidad = apiDBContext.Paises.ToList().Where(x => x.Id == usuario.Nacionalidad).First().Nombre,
                    Estado = usuario.Estado,
                    Avatar = apiDBContext.Avatar.ToList().Where(x => x.Id == usuario.Avatar).First().Imagen,
                    Actividad = apiDBContext.Actividad.ToList().Where(x => x.Id == usuario.Id_actividad).First().Nombre_act,
                    Ranking = usuario.Ranking,
                    Monedas = usuario.Monedas

                };

                list_return.Add(uApi);
            }

            return Ok(list_return);
        }

        [HttpGet]
        [Route("lista/jugadores")]
        public IActionResult GetAllPlayers()
        {
            List<Usuario> list_usuario = apiDBContext.Usuario.ToList().Where(x => x.Administrador == false).ToList();
            List<UsuarioAPI> list_return = new List<UsuarioAPI>();
            foreach (Usuario usuario in list_usuario)
            {
                UsuarioAPI uApi = new UsuarioAPI()
                {
                    Id = usuario.Id,
                    Administrador = usuario.Administrador,
                    Nombre = usuario.Nombre,
                    Username = usuario.Username,
                    Contrasena = usuario.Contrasena,
                    Correo = usuario.Correo,
                    Nacionalidad = apiDBContext.Paises.ToList().Where(x => x.Id == usuario.Nacionalidad).First().Nombre,
                    Estado = usuario.Estado,
                    Avatar = apiDBContext.Avatar.ToList().Where(x => x.Id == usuario.Avatar).First().Imagen,
                    Actividad = apiDBContext.Actividad.ToList().Where(x => x.Id == usuario.Id_actividad).First().Nombre_act,
                    Ranking = usuario.Ranking,
                    Monedas = usuario.Monedas

                };

                list_return.Add(uApi);
            }

            return Ok(list_return);
        }

        [HttpGet]
        [Route("get/{Id}")]
        public IActionResult Get([FromRoute] string Id)
        {
            Usuario usuario = apiDBContext.Usuario.ToList().Where(x => x.Id == Id).First();
            UsuarioAPI uApi = new UsuarioAPI()
            {
                Id = usuario.Id,
                Administrador = usuario.Administrador,
                Nombre = usuario.Nombre,
                Username = usuario.Username,
                Contrasena = usuario.Contrasena,
                Correo = usuario.Correo,
                Nacionalidad = apiDBContext.Paises.ToList().Where(x => x.Id == usuario.Nacionalidad).First().Nombre,
                Estado = usuario.Estado,
                Avatar = apiDBContext.Avatar.ToList().Where(x => x.Id == usuario.Avatar).First().Imagen,
                Actividad = apiDBContext.Actividad.ToList().Where(x => x.Id == usuario.Id_actividad).First().Nombre_act,
                Ranking = usuario.Ranking,
                Monedas = usuario.Monedas

            };
            return Ok(uApi);
        }

        [HttpPost]
        [Route("guardarJugador")]
        public IActionResult SaveUsuario(UsuarioAPI usuarioAPI)
        {
            Usuario usuario = new Usuario()
            {
                Id = GeneratorID.GenerateRandomId("U-"),
                Administrador = false,
                Nombre = usuarioAPI.Nombre,
                Username = usuarioAPI.Username,
                Contrasena = usuarioAPI.Contrasena,
                Correo = usuarioAPI.Correo,
                Nacionalidad = apiDBContext.Paises.ToList().Where(x => x.Nombre == usuarioAPI.Nacionalidad).First().Id,
                Estado = true,
                Avatar = 1,//apiDBContext.Avatar.ToList().Where(x => x.Imagen == usuarioAPI.Avatar).First().Id,
                Ranking = usuarioAPI.Ranking,
                Monedas = 20,
                Id_actividad = 1
            };

            apiDBContext.Usuario.Add(usuario);

            apiDBContext.SaveChanges();

            return Ok(usuario);
        }

        [HttpDelete]
        [Route("delete/{Id}")]
        public IActionResult DeleteUsario([FromRoute] string Id)
        {
            Usuario usuarioDelete = apiDBContext.Usuario.ToList().Where(x => x.Id == Id).First();
            apiDBContext.Usuario.Remove(usuarioDelete);
            apiDBContext.SaveChanges();
            return Ok(usuarioDelete);
        }
    }
}
