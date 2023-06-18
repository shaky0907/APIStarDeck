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
            MockUpDataBase mock = new MockUpDataBase();
            var databaseContext = mock.GetDatabaseContext();

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
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async void GetUsuario_ReturnUsuario()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var controller = new UsuarioData(dbContext);

            //Act
            var result = controller.getUsuario("1");

            //Assert
            Assert.NotNull(result);
            Assert.Equal("1", result.Id);

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
            Assert.Equal(2, result.Count());

        }



        [Fact]
        public async void addUsuario()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var controller = new UsuarioData(dbContext);

            //Act 
            controller.addUsuario(new UsuarioAPI()
            {
                Id = "1",
                Administrador =false,
                Nombre = "Nuevo Usuario",
                Username = "username",
                Contrasena = "12345678",
                Correo = "Correo",
                Nacionalidad = "pais 1",
                Estado = true,
                Avatar = "fewfefaewdgtryntrhbrty",
                Ranking =100,
                Monedas = 100,
                Actividad = "No busca partida"
            });

            var result = controller.getJugadores();

            //Assert

            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async void deleteUsuario()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var controller = new UsuarioData(dbContext);


            //act 

            controller.deleteUsuario("1");
            var result = controller.getJugadores();

            //Assert 
            Assert.Equal(1, result.Count());
        }

        

    }
}
