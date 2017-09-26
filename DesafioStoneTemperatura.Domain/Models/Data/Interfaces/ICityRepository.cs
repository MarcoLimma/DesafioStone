using System.Collections.Generic;
using DesafioStoneTemperatura.Domain.Models.Api;

namespace DesafioStoneTemperatura.Domain.Models.Data.Interfaces
{
    public interface ICityRepository
    {
        void Add(City city);
        void Remove(string name);
        List<City> GetAll();
        City GetByName(string name);
        CityDataContract GetTemperatures(string name, int listSize);
        void DeleteTemperatures(string name);
        List<CityTemperatureDataContract> GetLatestByTemperatureRegistered(int page);
    }
}