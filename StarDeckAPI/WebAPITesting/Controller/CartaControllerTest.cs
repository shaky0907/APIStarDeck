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
using System.Security.Cryptography.Xml;
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
                .UseSqlServer("Server = DAVIDGAMING ; Database=StarDeckTest; Trusted_Connection=True ;  TrustServerCertificate=True")
                .Options;

            var databaseContext = new APIDbContext(options);

            
            return databaseContext;
        }


        [Fact]
        public async void GetAllCartas_ReturnsCartas()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var cartaController = new CartaData(dbContext);

            //Act
            var result = cartaController.getAllCartas();
            
            //Assert
            Assert.NotNull(result);
            Assert.Equal(27, result.Count());
        }

        [Fact]
        public async void GetCarta_ReturnCarta()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var cartaController = new CartaData(dbContext);

            //Act
            var result = cartaController.getCartaDB("1");

            //Assert
            Assert.NotNull(result);
            Assert.Equal("1",result.Id);
        }

       
    }
}
