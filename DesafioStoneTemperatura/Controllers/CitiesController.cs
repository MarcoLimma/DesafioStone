using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DesafioStoneTemperatura.Data;
using DesafioStoneTemperatura.Data.Repositories;
using DesafioStoneTemperatura.Domain.Models;

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
        [Route("cities")]
        // GET /cities
        public IEnumerable<object> Get()
        {
            return cityRepo.GetAll().ToList();
        }

        [HttpGet]
        [Route("cities/{id}")]
        // GET /cities/5
        public object Get(Guid id)
        {
            return cityRepo.GetById(id);
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
        // GET /cities/{city_name}/temperatures
        public void DeleteTemperatures(string name)
        {
            cityRepo.DeleteTemperatures(name);
        }
    }
}
