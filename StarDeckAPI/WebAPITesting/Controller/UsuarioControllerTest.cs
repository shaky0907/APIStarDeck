using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarDeckAPI.Controllers;
using StarDeckAPI.Data;
using StarDeckAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WebAPITesting.Controller
{
    public class UsuarioControllerTest
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
        public async void GetUsuarios_ReturnUsuarios()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var controller = new UsuarioData(dbContext);

            //Act

            var result = controller.getUsuarios();

            //Assert

            Assert.NotNull(result);
            Assert.Equal(4, result.Count());
        }

        [Fact]
        public async void GetUsuario_ReturnUsuario()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var controller = new UsuarioData(dbContext);

            //Act
            var result = controller.getUsuario("U-KwW5aumoYDKX");

            //Assert
            Assert.NotNull(result);
            Assert.Equal("U-KwW5aumoYDKX", result.Id);

        }

        [Fact]
        public async void GetJugadores_ReturnJugadores()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var controller = new UsuarioData(dbContext);

            //Act
            var result = controller.getJugadores();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(4, result.Count());

        }
    }
}
