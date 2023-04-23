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
    [Route("carta")]
    [ApiController]
    public class CartaController : Controller
    {
        private readonly IConfiguration _configuration;

        public CartaController(IConfiguration configuration)
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
            sb.Append("C-");

            while (sb.Length < idLength)
            {
                char c = (char) random.Next('0', 'z' + 1);
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
                    select * from dbo.Carta
                    ";


            string json = execquery(query);
            List<Carta> cartas = JsonConvert.DeserializeObject<List<Carta>>(json);

            List<CartaAPI> cartasapi = new List<CartaAPI>();

            foreach (var carta in cartas)
            {


                //Get Raza ID
                string queryraza = @"
                                select * from dbo.Raza
                                where Id = " + carta.Raza + @"
                                ";

                string jsonraza = execquery(queryraza);
                List<Raza> raza = JsonConvert.DeserializeObject<List<Raza>>(jsonraza);
                string razaName = "";

                if (raza.Count() != 0)
                {
                    razaName = raza[0].Nombre;
                }



                string querytipo = @"
                                select * from dbo.Tipo
                                where Id = " + carta.Tipo + @"
                                ";

                string jsontipo = execquery(querytipo);
                List<Tipo> tipo = JsonConvert.DeserializeObject<List<Tipo>>(jsontipo);

                string tipoName = "";
                if (tipo.Count() != 0)
                {
                    tipoName = tipo[0].Nombre;
                }

                CartaAPI c = new CartaAPI();
                c.Id = carta.Id;
                c.Nombre = carta.N_Personaje;
                c.Energia = carta.Energia;
                c.Costo = carta.C_batalla;
                c.Imagen = carta.Imagen;
                c.Estado = carta.Activa;
                c.Descripcion = carta.Descripcion;
                c.Raza = razaName;
                c.Tipo = tipoName;

                cartasapi.Add(c);

            }


            json = JsonConvert.SerializeObject(cartasapi, Formatting.Indented);

            return json;


           

        }

        [HttpGet]
        [Route("lista/{id}")]
        public string Get(string id)
        {
            string query = @"
                    select * from dbo.Carta
                    where ID = '" + id + @"'
                    ";

            string json = execquery(query);
            List<Carta> cartas = JsonConvert.DeserializeObject<List<Carta>>(json);

            List<CartaAPI> cartasapi = new List<CartaAPI>();

            foreach (var carta in cartas)
            {
                

                //Get Raza ID
                string queryraza = @"
                                select * from dbo.Raza
                                where Id = " + carta.Raza + @"
                                ";

                string jsonraza = execquery(queryraza);
                List<Raza> raza = JsonConvert.DeserializeObject<List<Raza>>(jsonraza);
                string razaName = "";

                if (raza.Count() != 0)
                {
                    razaName = raza[0].Nombre;
                }



                string querytipo = @"
                                select * from dbo.Tipo
                                where Id = " + carta.Tipo + @"
                                ";

                string jsontipo = execquery(querytipo);
                List<Tipo> tipo = JsonConvert.DeserializeObject<List<Tipo>>(jsontipo);

                string tipoName ="";
                if (tipo.Count() != 0)
                {
                    tipoName = tipo[0].Nombre;
                }

                CartaAPI c = new CartaAPI();
                c.Id = carta.Id;
                c.Nombre = carta.N_Personaje;
                c.Energia = carta.Energia;
                c.Costo = carta.C_batalla;
                c.Imagen = carta.Imagen;
                c.Estado = carta.Activa;
                c.Descripcion = carta.Descripcion;
                c.Raza = razaName;
                c.Tipo = tipoName;

                cartasapi.Add(c);

            }


            json = JsonConvert.SerializeObject(cartasapi, Formatting.Indented);

            return json;

        }

        [HttpPost]
        [Route("guardar")]
        public JsonResult Post(CartaAPI cartaapi)
        {
            
            //Create ID
            string Id = GenerateRandomId();


            //Get Raza ID
            string queryraza = @"
                                select * from dbo.Raza
                                where Nombre = '" + cartaapi.Raza + @"'
                                ";

            string jsonraza = execquery(queryraza);
            List<Raza> raza = JsonConvert.DeserializeObject<List<Raza>>(jsonraza);
            int razaint = 0;

            if (raza.Count() != 0)
            {
                razaint = raza[0].Id;
            }

            string querytipo = @"
                                select * from dbo.Tipo
                                where Nombre = '" + cartaapi.Tipo + @"'
                                ";

            string jsontipo = execquery(querytipo);
            List<Tipo> tipo = JsonConvert.DeserializeObject<List<Tipo>>(jsontipo);

            int tipoint = 0;
            if (tipo.Count() != 0)
            {
                tipoint = tipo[0].Id;
            }



            string query = @"
                    insert into dbo.carta values 
                    ('" + Id + "','" + cartaapi.Nombre + "'," + cartaapi.Energia + "," + cartaapi.Costo  +",'" + cartaapi.Imagen + "'," + razaint + ",'" + cartaapi.Estado + "','" + cartaapi.Descripcion + "'," + tipoint + @")
                    ";


            string json = execquery(query);

            return new JsonResult("Added Successfully");
        }






        [HttpDelete]
        [Route("delete/{id}")]
        public JsonResult Delete(string id)
        {
            string query = @"
                    delete from dbo.Carta
                    where Id = '" + id + @"'
                    ";

            string json = execquery(query);

            return new JsonResult("Deleted Successfully");
        }

    }
}
