using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DesafioStoneTemperatura.Data;
using DesafioStoneTemperatura.Data.Repositories;
using DesafioStoneTemperatura.Domain.Models;
using DesafioStoneTemperatura.Helpers;

namespace DesafioStoneTemperatura.Controllers
{
    public class CitiesController : ApiController
    {
        private DataContext context;
        private CityRepository cityRepo;

        public CitiesController()
        {
            this.context = new DataContext();
            this.cityRepo = new CityRepository(context);
        }

        [HttpGet]
        [Route("cities/{name}/temperatures")]
        // GET /cities/{name}/temperature
        public object Get(string name)
        {
            return cityRepo.GetTemperatures(name);
        }

        [HttpPost]
        [Route("cities/{name}")]
        // POST /cities/{name}
        public void Post(string name)
        {
            cityRepo.Add(new City(name));
        }

        [HttpDelete]
        [Route("cities/{name}")]
        // DELETE /cities/{name}
        public void Delete(string name)
        {
            cityRepo.Remove(name);
        }

        [HttpDelete]
        [Route("cities/{name}/temperatures")]
        // DELETE /cities/{city_name}/temperatures
        public void DeleteTemperatures(string name)
        {
            cityRepo.DeleteTemperatures(name);
        }

        [HttpGet]
        [Route("temperatures")]
        // GET /cities
        public object GetBuTemperatures()
        {
            return cityRepo.GetLatestByTemperatureRegistered(1);
        }

        [HttpGet]
        [Route("temperatures/{page}")]
        // GET /cities
        public object GetBuTemperatures(int page)
        {
            return cityRepo.GetLatestByTemperatureRegistered(page);
        }

        [HttpPost]
        [Route("cities/by_cep/{cep}")]
        // POST /cities/by_cep/{cep}
        public void PostByCep(string cep)
        {
            string name = new CepHelper().GetCityName(cep);

            cityRepo.Add(new City(name));
        }
    }
}
