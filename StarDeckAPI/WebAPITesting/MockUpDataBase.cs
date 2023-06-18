using Microsoft.EntityFrameworkCore;
using StarDeckAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPITesting
{
    public class MockUpDataBase
    {

        public APIDbContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<APIDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new APIDbContext(options);
            databaseContext.Database.EnsureCreated();

            return databaseContext;
        }

        public void addCartas() 
        {
        
        }

        public void addRazas() { }
        public void addTipo() { }



    }
}
