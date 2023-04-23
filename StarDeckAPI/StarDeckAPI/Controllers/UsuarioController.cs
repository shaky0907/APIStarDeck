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
        [Route("lista")]
        public string Get()
        {
            string query = @"
                    select * from dbo.Usuario
                    ";


            string json = execquery(query);
            List<Usuario> usuarios = JsonConvert.DeserializeObject<List<Usuario>>(json);
            json = JsonConvert.SerializeObject(usuarios, Formatting.Indented);


            return json;

        }


        [HttpPost]
        [Route("guardar")]
        public JsonResult Post(Usuario usuario)
        {
            //crear Id
            usuario.Id = GenerateRandomId();
            //


            /*
            string query = @"
                    insert into dbo.Usuario values 
                    ('" + usuario.Id + "','" + usuario.Administrador + "','" + usuario.Nombre + "','" + usuario.Contrasena + "','" + usuario.Correo + "','" +  + "','" + true + "','" + usuario.Descripcion + "','" + usuario.Tipo + @"')
                    ";

            */
            //string json = execquery(query);
            
            return new JsonResult(usuario.Id);
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
