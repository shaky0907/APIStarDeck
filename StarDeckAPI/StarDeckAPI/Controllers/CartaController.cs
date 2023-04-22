using System;
using System.Collections.Generic;


using Microsoft.AspNetCore.Mvc;

using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

using StarDeckAPI.Models;

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

        [HttpGet]
        [Route("lista")]
        public string Get()
        {
            string query = @"
                    select * from dbo.Carta
                    ";


            string json = execquery(query);
            List<Carta> cartas = JsonConvert.DeserializeObject<List<Carta>>(json);
            json = JsonConvert.SerializeObject(cartas, Formatting.Indented);


            return json;

        }

        [HttpPost]
        [Route("guardar")]
        public JsonResult Post(Carta carta)
        {
            string query = @"
                    insert into dbo.carta values 
                    ('" + carta.Id + "','" + carta.N_Personaje + "','" + carta.Energia + "','" + carta.C_batalla  +"','" + carta.Imagen + "','" + carta.Raza + "','" + carta.Activa + "','" + carta.Descripcion + "','" + carta.Tipo + @"')
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
