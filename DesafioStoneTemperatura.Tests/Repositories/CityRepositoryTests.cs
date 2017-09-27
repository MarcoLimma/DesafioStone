using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DesafioStoneTemperatura.Data;
using DesafioStoneTemperatura.Data.Repositories;
using DesafioStoneTemperatura.Domain.Models.Data;
using DesafioStoneTemperatura.Domain.Models.Data.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DesafioStoneTemperatura.Tests.Repositories
{
    [TestClass()]
    public class CityRepositoryTests
    {
        private Mock<DataContext> _mockDataContext;
        private Mock<DbSet<City>> _mockCitySet;
        private Mock<DbSet<Temperature>> _mockTemperatureSet;
        private ICityRepository cityRepository;
        private List<City> cities;
        private string cityName;

        [TestInitialize]
        public void RepositoryInit()
        {
            cityName = "Rio de Janeiro";

            _mockDataContext = new Mock<DataContext>();
            _mockCitySet = new Mock<DbSet<City>>();
            _mockTemperatureSet = new Mock<DbSet<Temperature>>();
            cityRepository = new CityRepository(_mockDataContext.Object);

            var city1 = new City()
            {
                Id = Guid.NewGuid(),
                Name = "Rio de Janeiro",
            };

            var city2 = new City()
            {
                Id = Guid.NewGuid(),
                Name = "São Paulo",
            };

            var city3 = new City()
            {
                Id = Guid.NewGuid(),
                Name = "Fortaleza",
            };

            var city1Temperatures = new List<Temperature>()
            {
                new Temperature()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Value = 25,
                    CityId = city1.Id,
                    City = city1
                },
                new Temperature()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now.AddHours(-1),
                    Value = 26,
                    CityId = city1.Id,
                    City = city1
                },
                new Temperature()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now.AddHours(-2),
                    Value = 27,
                    CityId = city1.Id,
                    City = city1
                },
            };

            var city2Temperatures = new List<Temperature>()
            {
                new Temperature()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Value = 22,
                    CityId = city2.Id,
                    City = city2
                },
                new Temperature()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now.AddHours(-1),
                    Value = 23,
                    CityId = city2.Id,
                    City = city2
                },
                new Temperature()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now.AddHours(-2),
                    Value = 24,
                    CityId = city2.Id,
                    City = city2
                },
            };

            var city3Temperatures = new List<Temperature>()
            {
                new Temperature()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Value = 28,
                    CityId = city3.Id,
                    City = city3
                },
                new Temperature()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now.AddHours(-1),
                    Value = 29,
                    CityId = city3.Id,
                    City = city3
                },
                new Temperature()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now.AddHours(-2),
                    Value = 30,
                    CityId = city3.Id,
                    City = city3
                },
            };

            city1.Temperatures = city1Temperatures;
            city2.Temperatures = city2Temperatures;
            city3.Temperatures = city3Temperatures;

            cities = new List<City> {city1, city2, city3};

            List<Temperature> temperatures = cities.SelectMany(c => c.Temperatures).ToList();

            _mockCitySet = new Mock<DbSet<City>>();
            _mockCitySet.As<IQueryable<City>>()
                .Setup(x => x.Provider)
                .Returns(cities.AsQueryable().Provider);
            _mockCitySet.As<IQueryable<City>>()
                .Setup(x => x.ElementType)
                .Returns(cities.AsQueryable().ElementType);
            _mockCitySet.As<IQueryable<City>>()
                .Setup(x => x.Expression)
                .Returns(cities.AsQueryable().Expression);
            _mockCitySet.As<IQueryable<City>>()
                .Setup(x => x.GetEnumerator())
                .Returns(cities.GetEnumerator());

            _mockTemperatureSet = new Mock<DbSet<Temperature>>();
            _mockTemperatureSet.As<IQueryable<Temperature>>()
                .Setup(x => x.Provider)
                .Returns(temperatures.AsQueryable().Provider);
            _mockTemperatureSet.As<IQueryable<Temperature>>()
                .Setup(x => x.ElementType)
                .Returns(temperatures.AsQueryable().ElementType);
            _mockTemperatureSet.As<IQueryable<Temperature>>()
                .Setup(x => x.Expression)
                .Returns(temperatures.AsQueryable().Expression);
            _mockTemperatureSet.As<IQueryable<Temperature>>()
                .Setup(x => x.GetEnumerator())
                .Returns(temperatures.GetEnumerator());
        }

        [TestMethod()]
        public void AddTest()
        {
            _mockDataContext.Setup(x => x.Cities).Returns(_mockCitySet.Object);

            cityRepository.Add(new City("Cabo Frio"));

            _mockCitySet.Verify(m => m.Add(It.IsAny<City>()), Times.Once());
            _mockDataContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod()]
        public void RemoveTest()
        {
            _mockDataContext.Setup(c => c.Cities).Returns(_mockCitySet.Object);
            _mockCitySet.Setup(m => m.Remove(It.IsAny<City>())).Callback<City>((entity) => cities.Remove(entity));

            cityRepository.Remove(cityName);

            _mockDataContext.VerifyGet(x => x.Cities, Times.Exactly(2));
            _mockDataContext.Verify(x => x.SaveChanges(), Times.Once());

            Assert.AreEqual(cities.Count, 2);
            Assert.IsFalse(cities.Any(x => x.Name == cityName));
        }

        [TestMethod()]
        public void GetAllTest()
        {
            _mockDataContext.Setup(c => c.Cities).Returns(_mockCitySet.Object);

            var cities = cityRepository.GetAll();

            Assert.AreEqual(cities.Count, 3);
        }

        [TestMethod()]
        public void GetByNameTest()
        {
            _mockDataContext.Setup(c => c.Cities).Returns(_mockCitySet.Object);

            var cityName = "Rio de Janeiro";

            var city = cityRepository.GetByName(cityName);

            Assert.AreEqual(cityName, city.Name);
        }

        [TestMethod()]
        public void GetTemperaturesTest()
        {
            _mockDataContext.Setup(c => c.Cities).Returns(_mockCitySet.Object);

            var cityDataContract = cityRepository.GetTemperatures(cityName, 3);

            Assert.AreEqual(cityDataContract.city, cityName);
            Assert.AreEqual(cityDataContract.temperatures.Count, 3);
        }

        [TestMethod()]
        public void DeleteTemperaturesTest()
        {
            _mockDataContext.Setup(c => c.Cities).Returns(_mockCitySet.Object);
            _mockDataContext.Setup(c => c.Temperatures).Returns(_mockTemperatureSet.Object);
            _mockTemperatureSet.Setup(m => m.RemoveRange(It.IsAny<List<Temperature>>())).Callback<IEnumerable<Temperature>>((entities) => cities.FirstOrDefault(x => x.Name == cityName).Temperatures.RemoveRange(0, entities.Count()));

            cityRepository.DeleteTemperatures(cityName);

            Assert.AreEqual(cities.FirstOrDefault(x => x.Name == cityName).Temperatures.Count, 0);
        }

        [TestMethod()]
        public void GetLatestByTemperatureRegisteredTest()
        {
            _mockDataContext.Setup(c => c.Temperatures).Returns(_mockTemperatureSet.Object);

            var cityDataContracts = cityRepository.GetLatestByTemperatureRegistered(1);

            Assert.AreEqual(cityDataContracts[0].city, "Fortaleza");
            Assert.AreEqual(cityDataContracts[0].temperature, 28);
            Assert.AreEqual(cityDataContracts[1].city, "Rio de Janeiro");
            Assert.AreEqual(cityDataContracts[1].temperature, 25);
            Assert.AreEqual(cityDataContracts[2].city, "São Paulo");
            Assert.AreEqual(cityDataContracts[2].temperature, 22);
        }
    }
}