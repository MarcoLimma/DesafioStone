using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DesafioStoneTemperatura.Domain.Models.Data;

namespace DesafioStoneTemperatura.Data.Repositories
{
    public class CityRepository
    {
        private DataContext context;

        public CityRepository(DataContext _context)
        {
            this.context = _context;
        }

        public void Add(City city)
        {
                context.Cities.Add(city);
                context.SaveChanges();
        }

        public void Remove(string name)
        {
            City city = GetByName(name);

            if (city != null)
            {
                context.Cities.Remove(city);
                context.SaveChanges();
            }
            
        }

        public List<City> GetAll()
        {
            return context.Cities.ToList();
        }

        public City GetById(Guid id)
        {
            return context.Cities.FirstOrDefault(city => city.Id == id);
        }

        public City GetByName(string name)
        {
            return context.Cities.FirstOrDefault(city => city.Name == name);
        }

        //ToDo: Criar modelos para serem  retornados no lugar de objects
        public object GetTemperatures(string name, int listSize)
        {
            var city = context.Cities.FirstOrDefault(c => c.Name == name);

            if (city == null)
                return null;

            return new {
                city = city.Name,
                temperatures = city.Temperatures
                    .OrderByDescending(t => t.Date)
                    .Take(listSize)
                    .Select( t => new {
                        date = t.Date.ToString("yyyy-MM-dd HH:mm:ss"),
                        temperature = t.Value
                    })
            };
        }

        public void DeleteTemperatures(string name)
        {
            City city = GetByName(name);

            if (city != null)
            {
                context.Temperatures.RemoveRange(city.Temperatures);
                context.SaveChanges();
            }

        }


        public object GetLatestByTemperatureRegistered(int page)
        {
            int pageSize = 10;

            object cities = context.Temperatures
                    //Pega as últimas temperaturas registradas
                    .OrderByDescending(t => t.Date)
                    //Seleciona as cidades dessas temperaturas, com distinct
                    .Select(t => t.City).Distinct()
                    .Select( c => new {
                        city = c.Name,
                        //Pega a última temperatura registrada da cidade
                        temperature = c.Temperatures
                            .OrderByDescending(t => t.Date)
                            .Select( t => t.Value)
                            .FirstOrDefault()
                    })
                    //Ordena as cidades pela temperatura e depois pagina
                    .OrderByDescending(t => t.temperature)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

            return cities;
        }

    }
}


