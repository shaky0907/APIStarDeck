﻿using FluentAssertions;
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
            
            MockUpDataBase mock = new MockUpDataBase();
            var databaseContext = mock.GetDatabaseContext();

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
            Assert.Equal(18, result.Count());
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

        [Fact]
        public async void AddCarta()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var cartaController = new CartaData(dbContext);

            //Act
            cartaController.guardarCartaDB(new CartaAPI()
            {
                Id = "1",
                Nombre = "Carta nueva",
                Energia = 20,
                Costo = 20,
                Imagen = "1211313",
                Raza = "1",
                Tipo = "1",
                Estado =true,
                Descripcion = "1",
            });


            var result = cartaController.getAllCartas();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(19, result.Count());
        }

        [Fact]
        public async void deleteCarta()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var cartaController = new CartaData(dbContext);

            //Act
            cartaController.deleteCartaDB("1");

            var result = cartaController.getAllCartas();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(17, result.Count());

        }
    }
}
