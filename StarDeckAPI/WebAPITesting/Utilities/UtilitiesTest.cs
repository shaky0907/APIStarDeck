using FluentAssertions;
using StarDeckAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPITesting.Utilities
{
    public class UtilitiesTest
    {


        [Fact]
        public void Utilities_GenerateID_ReturnsID()
        {
            //Arrange
            var C = "C-";
           


            //Act
            var id1 = GeneratorID.GenerateRandomId(C);
            var id2 = GeneratorID.GenerateRandomId(C);
            var id3 = GeneratorID.GenerateRandomId(C);
            var id4 = GeneratorID.GenerateRandomId(C);

            //Assert

            id1.Should().NotBeNull();
            id2.Should().NotBeNull();
            id3.Should().NotBeNull();
            id4.Should().NotBeNull();

            Assert.NotEqual(id1, id2);
            Assert.NotEqual(id1, id3);
            Assert.NotEqual(id1, id4);

            Assert.NotEqual(id2, id3);
            Assert.NotEqual(id2, id4);

            Assert.NotEqual(id3, id4);
        }
    }
}
