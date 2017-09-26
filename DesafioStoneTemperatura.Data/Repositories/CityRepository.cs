using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core;
using System.Linq;
using DesafioStoneTemperatura.Domain.Models.Api;
using DesafioStoneTemperatura.Domain.Models.Data;
using DesafioStoneTemperatura.Domain.Models.Data.Interfaces;

namespace DesafioStoneTemperatura.Data.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly DataContext context;

        public CityRepository(DataContext context)
        {
            this.context = context;
        }

        public CityRepository()
        {
            this.context = new DataContext();
        }

        public void Add(City city)
        {
            context.Cities.Add(city);
            context.SaveChanges();
        }

        public void Remove(string name)
        {
            var city = GetByName(name);

            if (city == null)
            {
                throw new ObjectNotFoundException();
            }

            context.Cities.Remove(city);
            context.SaveChanges();
        }

        public List<City> GetAll()
        {
            return context.Cities.ToList();
        }

        public City GetByName(string name)
        {
            return context.Cities.FirstOrDefault(city => string.Equals(city.Name, name, StringComparison.CurrentCultureIgnoreCase));
        }

        public CityDataContract GetTemperatures(string name, int listSize)
        {
            var city = GetByName(name);

            if (city == null)
            {
                throw new ObjectNotFoundException();
            }

            return new CityDataContract()
            {
                city = city.Name,
                temperatures = city.Temperatures
                    .OrderByDescending(t => t.Date)
                    .Take(listSize)
                    .Select(t => new TemperatureDataContract()
                    {
                        date = t.Date.ToString("yyyy-MM-dd HH:mm:ss"),
                        temperature = t.Value
                    }).ToList()
            };
        }

        public void DeleteTemperatures(string name)
        {
            var city = GetByName(name);

            if (city == null)
            {
                throw new ObjectNotFoundException();
            }

            context.Temperatures.RemoveRange(city.Temperatures);
            context.SaveChanges();
        }
        
        public List<CityTemperatureDataContract> GetLatestByTemperatureRegistered(int page)
        {
            var pageSize = 10;
            int.TryParse(ConfigurationManager.AppSettings["PageSize"], out pageSize);

            return context.Temperatures
                    //Pega as últimas temperaturas registradas
                    .OrderByDescending(t => t.Date)
                    //Seleciona as cidades dessas temperaturas, com distinct
                    .Select(t => t.City).Distinct()
                    .Select(c => new CityTemperatureDataContract()
                    {
                        city = c.Name,
                        //Pega a última temperatura registrada da cidade
                        temperature = c.Temperatures
                           .OrderByDescending(t => t.Date)
                           .Select(t => t.Value)
                           .FirstOrDefault()
                    })
                    //Ordena as cidades pela temperatura e depois pagina
                    .OrderByDescending(t => t.temperature)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
        }
    }
}