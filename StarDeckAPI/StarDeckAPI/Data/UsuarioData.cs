using Microsoft.AspNetCore.Mvc;
using StarDeckAPI.Models;
using StarDeckAPI.Utilities;

namespace StarDeckAPI.Data
{
    public class UsuarioData
    {
        private Random random = new Random();
        private APIDbContext apiDBContext;

        public UsuarioData(APIDbContext apiDBContext)
        {
            this.apiDBContext = apiDBContext;
        }

        public List<UsuarioAPI> getUsuarios()
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

            return list_return;
        }

        public List<UsuarioAPI> getJugadores()
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

            return list_return;
        }

        public UsuarioAPI getUsuario( string Id)
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
            return uApi;
        }

        public Usuario addUsuario(UsuarioAPI usuarioAPI)
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

            return usuario;
        }

        public Usuario deleteUsuario(string Id)
        {
            Usuario usuarioDelete = apiDBContext.Usuario.ToList().Where(x => x.Id == Id).First();
            apiDBContext.Usuario.Remove(usuarioDelete);
            apiDBContext.SaveChanges();
            return usuarioDelete;
        }
    }
}
