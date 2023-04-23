using System;
using System.Collections.Generic;


using Microsoft.AspNetCore.Mvc;

using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

using StarDeckAPI.Models;
using System.Text;


namespace StarDeckAPI.Controllers
{
    [Route("usuario")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IConfiguration _configuration;

        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string execquery(string query)
        {
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StarDeck");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            string json = JsonConvert.SerializeObject(table);
            return json;
        }

        public Random random = new Random();

        private string GenerateRandomId()
        {
            const int idLength = 14;
            StringBuilder sb = new StringBuilder(idLength);
            sb.Append("U-");

            while (sb.Length < idLength)
            {
                char c = (char)random.Next('0', 'z' + 1);
                if (Char.IsLetterOrDigit(c) && sb[sb.Length - 1] != c)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        [HttpGet]
        [Route("lista/jugador")]
        public string Get()
        {
            string query = @"
                    select * from dbo.Usuario
                    ";


            string json = execquery(query);
            List<Usuario> usuarios = JsonConvert.DeserializeObject<List<Usuario>>(json);
            List<UsuarioAPI> usuariosAPI = new List<UsuarioAPI>();

            foreach (var u in usuarios)
            {

                string querypais = @"
                                select * from dbo.Paises
                                where Id = " + u.Nacionalidad + @"
                                ";

                string jsonpais = execquery(querypais);
                List<Pais> pais = JsonConvert.DeserializeObject<List<Pais>>(jsonpais);
                string paisName = "";

                if (pais.Count() != 0)
                {
                    paisName = pais[0].Nombre;
                }

                string queryavatar = @"
                                select * from dbo.Avatar
                                where Id = '" + u.Avatar + @"'
                                ";

                string jsonavatar = execquery(queryavatar);
                List<Avatar> avatar = JsonConvert.DeserializeObject<List<Avatar>>(jsonavatar);
                string image = "";

                if (avatar.Count() != 0)
                {
                    image = avatar[0].Imagen;
                }



                UsuarioAPI usapi = new UsuarioAPI();
                usapi.Id = u.Id;
                usapi.Nombre = u.Nombre;
                usapi.Username = u.Username;
                usapi.Administrador = false;
                usapi.Contrasena = u.Contrasena;
                usapi.Estado = u.Estado;
                usapi.Nacionalidad = paisName;
                usapi.Avatar = image;
                usapi.Ranking = u.Ranking;
                usapi.Monedas = u.Monedas;
                usapi.Correo = u.Correo;

                usuariosAPI.Add(usapi);
            }



            json = JsonConvert.SerializeObject(usuariosAPI, Formatting.Indented);


            return json;

        }


        [HttpPost]
        [Route("guardarJugador")]
        public JsonResult Post(UsuarioAPI usuario)
        {
            //crear Id
            string Id = GenerateRandomId();


            string userquery = @"
                                select * from dbo.Usuario
                                where Username =  '" + usuario.Username +  @"'";
            string jsonusuarios = execquery(userquery);
            List<Usuario> usernames = JsonConvert.DeserializeObject<List<Usuario>>(jsonusuarios);

            string correoquery = @"
                                select * from dbo.Usuario
                                where Correo =  '" + usuario.Correo + @"'";
            string jsoncorreo = execquery(correoquery);
            List<Usuario> correos = JsonConvert.DeserializeObject<List<Usuario>>(jsoncorreo);

            if (correos.Count() != 0)
            {
                return new JsonResult("1");
            }
            else if( usernames.Count() != 0)
            {
                return new JsonResult("2");
            }
            else
            {
                //Get Pais ID
                string querypais = @"
                                select * from dbo.Paises
                                where Nombre = '" + usuario.Nacionalidad + @"'
                                ";

                string jsonpais = execquery(querypais);
                List<Pais> pais = JsonConvert.DeserializeObject<List<Pais>>(jsonpais);
                int paisint = 0;

                if (pais.Count() != 0)
                {
                    paisint = pais[0].Id;
                }


                string query = @"
                    insert into dbo.Usuario values 
                    ('" + Id + "','" + false + "','" + usuario.Nombre + "','" + usuario.Username + "','" + usuario.Contrasena + "','" + usuario.Correo + "'," + paisint + ",'" + true + "'," + 1 + "," + 0 + "," + 20 + @")
                    ";


                string json = execquery(query);

                return new JsonResult(Id);
            }
            
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public JsonResult Delete(string id)
        {
            string query = @"
                    delete from dbo.Usuario
                    where Id = '" + id + @"'
                    ";

            string json = execquery(query);

            return new JsonResult("Deleted Successfully");
        }

    }
}
