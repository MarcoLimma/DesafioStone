using Microsoft.VisualStudio.TestTools.UnitTesting;
using DesafioStoneTemperatura.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioStoneTemperatura.Controllers.Tests
{
    [TestClass()]
    public class CitiesControllerTests
    {

        [TestMethod()]
        public void GetTest()
        {
            //Arrange
            CitiesController controller = new CitiesController();

            // Act
            object result = controller.Get("fortaleza");

            // Assert
            Assert.IsNotNull(result);
            //Assert.AreEqual(2, result.Count());
            //Assert.AreEqual("value1", result.ElementAt(0));
            //Assert.AreEqual("value2", result.ElementAt(1));
        }

        [TestMethod()]
        public void PostTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTemperaturesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetByTemperaturesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetByTemperaturesPagesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PostByCepTest()
        {
            Assert.Fail();
        }
    }
}