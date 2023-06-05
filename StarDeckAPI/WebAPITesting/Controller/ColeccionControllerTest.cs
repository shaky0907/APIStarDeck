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
    public class ColeccionControllerTest
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
        public async void GetColeccionUsuario_ReturnColeccionUsuario()
        {
            //Arrange
            var databaseContext = await GetDatabaseContext();
            var controller = new ColeccionData(databaseContext);

            //Act
            var result = controller.getColeccionDelUsuario("U-KwW5aumoYDKX");

            //Assert
            Assert.NotNull(result);
            Assert.Equal(18,result.Count());
        }

        [Fact]
        public async void GetDecksUsuario_ReturnDecksUsuario()
        {
            //Arrange
            var databaseContext = await GetDatabaseContext();
            var controller = new ColeccionData(databaseContext);

            //Act
            var result = controller.getDecksUsuario("U-KwW5aumoYDKX");

            //Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Count());
        }

        [Fact]
        public async void GetDeckUsuario_ReturnDeckUsuario()
        {
            //Arrange
            var databaseContext = await GetDatabaseContext();
            var controller = new ColeccionData(databaseContext);

            //Act
            var result = controller.getDeckUsuario("D-JvzSwNmgAuv6");

            //Assert
            Assert.NotNull(result);
            Assert.Equal("D-JvzSwNmgAuv6", result.Id);
            Assert.Equal("U-KwW5aumoYDKX", result.Id_usuario);
        }

    }
}
