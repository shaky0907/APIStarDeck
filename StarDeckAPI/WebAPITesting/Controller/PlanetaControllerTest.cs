﻿using FluentAssertions;
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
    public class PlanetaControllerTest
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
        public async void GetPlanetas_ReturnsPlanetas()
        {
            //Arrange
            var context = await GetDatabaseContext();
            var controller = new PlanetaData(context);

            //Act
            var result = controller.getPlanetas();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async void GetPlaneta_ReturnPlaneta()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var planetaController = new PlanetaData(dbContext);

            //Act
            var result = planetaController.getPlaneta("1");

            //Assert
            Assert.NotNull(result);
            Assert.Equal("1", result.Id);
        }

    }
}
