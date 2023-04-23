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
                char c = (char)random.Next('0', 'z' + 1);
                if (Char.IsLetterOrDigit(c) && sb[sb.Length - 1] != c)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }


        [HttpGet]
        [Route("razas")]
        public string getrazas()
        {
            string query = @"
                    select * from dbo.Raza
                    ";


            string json = execquery(query);
            List<Raza> razas = JsonConvert.DeserializeObject<List<Raza>>(json);
            json = JsonConvert.SerializeObject(razas, Formatting.Indented);
            return json;
        }

        private List<Carta> GenerateRandomCartas(List<Carta> inputCards,int count)
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
        public string getnewCartas()
        {
            string queryBasicas = @"
                    select * from dbo.Carta
                    where Tipo = 5
                    ";

            //Conseguir 15 cartas basicas
            string jsonbasicas = execquery(queryBasicas);
            List<Carta> cartasbasicas = JsonConvert.DeserializeObject<List<Carta>>(jsonbasicas);

            List<Carta> cartasTotales = GenerateRandomCartas(cartasbasicas,15);

            string queryrn = @"
                    select * from dbo.Carta
                    where Tipo = 3 OR Tipo = 4
                    ";

            string jsonrn = execquery(queryrn);
            List<Carta> cartasrn = JsonConvert.DeserializeObject<List<Carta>>(jsonrn);

            List<Carta> cartasrestantes = GenerateRandomCartas(cartasrn, 9);



            cartasTotales.AddRange(cartasrestantes);

            List<CartaAPI> cartasapi = new List<CartaAPI>();

            foreach (var c in cartasTotales)
            {


                //Get Raza ID
                string queryraza = @"
                                select * from dbo.Raza
                                where Id = " + c.Raza + @"
                                ";

                string jsonraza = execquery(queryraza);
                List<Raza> raza = JsonConvert.DeserializeObject<List<Raza>>(jsonraza);
                string razaName = "";

                if (raza.Count() != 0)
                {
                    razaName = raza[0].Nombre;
                }
                //get Tipo ID
                string querytipo = @"
                                select * from dbo.Tipo
                                where Id = " + c.Tipo + @"
                                ";

                string jsontipo = execquery(querytipo);
                List<Tipo> tipo = JsonConvert.DeserializeObject<List<Tipo>>(jsontipo);

                string tipoName = "";
                if (tipo.Count() != 0)
                {
                    tipoName = tipo[0].Nombre;
                }


                CartaAPI capi = new CartaAPI();

                capi.Id = c.Id;
                capi.Nombre = c.N_Personaje;
                capi.Energia = c.Energia;
                capi.Costo = c.C_batalla;
                capi.Imagen = c.Imagen;
                capi.Estado = c.Activa;
                capi.Descripcion = c.Descripcion;
                capi.Raza = razaName;
                capi.Tipo = tipoName;

                cartasapi.Add(capi);

            }

            string json = JsonConvert.SerializeObject(cartasapi, Formatting.Indented);

            return json;
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
