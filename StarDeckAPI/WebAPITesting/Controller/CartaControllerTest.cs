using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarDeckAPI.Controllers;
using StarDeckAPI.Data;
using StarDeckAPI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebAPITesting.Controller
{
    public class CartaControllerTest
    {
        private async Task<APIDbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<APIDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new APIDbContext(options);

            databaseContext.Database.EnsureCreated();

            if(await databaseContext.Carta.CountAsync() <= 0)
            {
                for(int i = 0; i < 3; i++)
                {
                    databaseContext.Carta.Add(
                    new Carta()
                    {
                        Id = "C-123141241"+i.ToString(),
                        N_Personaje = "Carta"+i.ToString(),
                        Energia =5,
                        Imagen = "imagen"+i.ToString(),
                        Raza = 1,
                        Activa = true,
                        Descripcion = "Carta Descripcion",
                        Tipo = 1
                    });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }


        [Fact]
        public async void CartaController_GetAllCartas_ReturnsCartas()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var cartaController = new CartaController(dbContext);

            //Act
            var result = cartaController.testFunc();


            //Assert
            result.Should().NotBeNull();
        }


    }
}
