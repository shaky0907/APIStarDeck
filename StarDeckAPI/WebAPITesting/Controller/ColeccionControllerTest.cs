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
            MockUpDataBase mock = new MockUpDataBase();
            var databaseContext = mock.GetDatabaseContext();

            return databaseContext;
        }



        [Fact]
        public async void GetColeccionUsuario_ReturnColeccionUsuario()
        {
            //Arrange
            var databaseContext = await GetDatabaseContext();
            var controller = new ColeccionData(databaseContext);

            //Act
            var result = controller.getColeccionDelUsuario("1");

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
            var result = controller.getDecksUsuario("1");

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
            var result = controller.getDeckUsuario("1");

            //Assert
            Assert.NotNull(result);
            Assert.Equal("1", result.Id);
            Assert.Equal("1", result.Id_usuario);
        }

        [Fact]
        public async void AddDeck()
        {
            //Arrange
            var databaseContext = await GetDatabaseContext();
            var controller = new ColeccionData(databaseContext);
            var cartacontroller = new CartaData(databaseContext);
            //Act
            var cartas = cartacontroller.getAllCartas();


            controller.addDeckUsuario(new DeckAPI()
            {
                Nombre = "Deck Nuevo",
                Estado = true,
                Id_usuario = "1",
                Cartas = cartas
            });

            var result = controller.getDecksUsuario("1");

            //Assert 
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }


        [Fact]
        public async void RemoveDeck() 
        {
            //Arrange
            var databaseContext = await GetDatabaseContext();
            var controller = new ColeccionData(databaseContext);

            //Act
            controller.deleteDeck("1");

            var result = controller.getDecksUsuario("1");

            //Assert
            Assert.Equal(0, result.Count());
        }

    }
}
