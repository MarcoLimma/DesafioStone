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
    public class TemperatureRepositoryTests
    {
        private Mock<DataContext> _mockDataContext;
        private Mock<DbSet<Temperature>> _mockTemperatureSet;
        private ITemperatureRepository temperatureRepository;
        private List<Temperature> temperatures;

        [TestInitialize]
        public void RepositoryInit()
        {
            _mockDataContext = new Mock<DataContext>();
            _mockTemperatureSet = new Mock<DbSet<Temperature>>();
            temperatureRepository = new TemperatureRepository(_mockDataContext.Object);


            temperatures = new List<Temperature>()
            {
                new Temperature()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Value = 22
                },
                new Temperature()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now.AddHours(-1),
                    Value = 23
                },
                new Temperature()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now.AddHours(-2),
                    Value = 24
                },
                new Temperature()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Value = 28
                },
                new Temperature()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now.AddHours(-1),
                    Value = 29
                },
                new Temperature()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now.AddHours(-2),
                    Value = 30
                },
            };

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
            _mockDataContext.Setup(x => x.Temperatures).Returns(_mockTemperatureSet.Object);

            temperatureRepository.Add(new Temperature(32, Guid.NewGuid()));

            _mockTemperatureSet.Verify(m => m.Add(It.IsAny<Temperature>()), Times.Once());
            _mockDataContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod()]
        public void RemoveTest()
        {
            _mockDataContext.Setup(c => c.Temperatures).Returns(_mockTemperatureSet.Object);
            _mockTemperatureSet.Setup(m => m.Remove(It.IsAny<Temperature>())).Callback<Temperature>((entity) => temperatures.Remove(entity));

            temperatureRepository.Remove(temperatures[0]);

            _mockDataContext.Verify(x => x.SaveChanges(), Times.Once());

            Assert.AreEqual(temperatures.Count, 5);
            Assert.IsFalse(temperatures.Any(x => x.Value == 22));
        }
    }
}