using System.Collections.Generic;
using DesafioStoneTemperatura.Data;
using DesafioStoneTemperatura.Data.Repositories;
using DesafioStoneTemperatura.TaskTemperature.Helpers;
using DesafioStoneTemperatura.Domain.Models.Data;

namespace DesafioStoneTemperatura.TaskTemperature
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new DataContext();
            var cityRepo = new CityRepository(context);
            var temperatureRepo = new TemperatureRepository(context);

            List<City> cities = cityRepo.GetAll();
            
            if (cities.Count > 0)
            {
                foreach (var city in cities)
                {
                    var temperature = WeatherApiHelper.GetTemperature(city);

                    temperatureRepo.Add(temperature);
                }
            }
        }
    }
}
