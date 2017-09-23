using System.Collections.Generic;
using DesafioStoneTemperatura.Data;
using DesafioStoneTemperatura.Data.Repositories;
using DesafioStoneTemperatura.Domain.Models;
using DesafioStoneTemperatura.TaskTemperature.Helpers;

namespace DesafioStoneTemperatura.TaskTemperature
{
    class Program
    {
        static void Main(string[] args)
        {
            DataContext context = new DataContext();
            CityRepository cityRepo = new CityRepository(context);
            TemperatureRepository temperatureRepo = new TemperatureRepository(context);

            List<City> cities = cityRepo.GetAll();

          
            if (cities.Count > 0)
            {
                foreach (var city in cities)
                {
                    WeatherApiHelper temperatureApi = new WeatherApiHelper();
                    Temperature temperature = temperatureApi.GetTemperature(city);

                    temperatureRepo.Add(temperature);
                }
            }
        }
    }
}
