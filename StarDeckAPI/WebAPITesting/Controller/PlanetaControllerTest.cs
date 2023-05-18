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
            var options = new DbContextOptionsBuilder<APIDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new APIDbContext(options);

            databaseContext.Database.EnsureCreated();

            if (await databaseContext.Planeta.CountAsync() <= 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    databaseContext.Planeta.Add(
                    new Planeta()
                    {
                        Id = "P-123141241" + i.ToString(),
                        Nombre = "Planeta " + i.ToString(),
                        Imagen = "Planeta " + i.ToString(),
                        Tipo = 1,
                        Estado = true,
                        Descripcion = "Planeta Descripcion",
                    });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }




        [Fact]
        public async void PlanetaController_GetPlanetas_ReturnsPlanetas()
        {
            //Arrange
            var context = await GetDatabaseContext();
            var controller = new PlanetaController(context);

            //Act
            var result = controller.GetPlanetas();

            //Assert
            Assert.NotNull(result);
        }

    }
}
