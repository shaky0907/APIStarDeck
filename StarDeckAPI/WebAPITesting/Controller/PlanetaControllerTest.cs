using FluentAssertions;
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
            MockUpDataBase mock = new MockUpDataBase();
            var databaseContext = mock.GetDatabaseContext();

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
            Assert.Equal(5, result.Count());
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

        [Fact]
        public async void createPlaneta()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var planetaController = new PlanetaData(dbContext);

            //Act

            planetaController.addPlaneta(new PlanetaAPI()
            {
                Nombre = "Nuevo planeta",
                Tipo=1,
                Descripcion = "Nuevo planeta",
                Estado =true,
                Imagen = "0000"
            });

            var result = planetaController.getPlanetas();

            //Assert
            Assert.Equal(6, result.Count());
        }

        [Fact]
        public async void deletePlaneta()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var planetaController = new PlanetaData(dbContext);

            //Act

            planetaController.deletePlaneta("1");
            
            var result = planetaController.getPlanetas();

            //Assert
            Assert.Equal(4, result.Count());
        }

    }
}
