using System.Web.Http;
using DesafioStoneTemperatura.Data;
using DesafioStoneTemperatura.Data.Repositories;
using DesafioStoneTemperatura.Domain.Models.Data;
using DesafioStoneTemperatura.Helpers;
using DesafioStoneTemperatura.Domain.Models.Api;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using DesafioStoneTemperatura.Domain.Models.Data.Interfaces;

namespace DesafioStoneTemperatura.Controllers
{
    public class CitiesController : ApiController
    {
        private ICityRepository cityRepo;

        public CitiesController()
        {
            var context = new DataContext();
            this.cityRepo = new CityRepository(context);
        }

        public CitiesController(ICityRepository _cityRepo)
        {
            this.cityRepo = _cityRepo;
        }

        // GET /cities/{name}/temperature
        [HttpGet]
        [Route("cities/{name}/temperatures")]
        public IHttpActionResult Get(string name)
        {
            try
            {
                return Ok(cityRepo.GetTemperatures(name, 30));
            }
            catch (ObjectNotFoundException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // POST /cities/{name}
        [HttpPost]
        [Route("cities/{name}")]
        public IHttpActionResult Post(string name)
        {
            try
            {
                cityRepo.Add(new City(name));
                return Ok($"'{name}' was successfully added.");
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // DELETE /cities/{name}
        [HttpDelete]
        [Route("cities/{name}")]
        public IHttpActionResult Delete(string name)
        {
            try
            {
                cityRepo.Remove(name);
                return Ok($"'{name}' was successfully deleted.");
            }
            catch (ObjectNotFoundException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // DELETE /cities/{city_name}/temperatures
        [HttpDelete]
        [Route("cities/{name}/temperatures")]
        public IHttpActionResult DeleteTemperatures(string name)
        {
            try
            {
                cityRepo.DeleteTemperatures(name);
                return Ok($"The temperature history of '{name}' was successfully deleted.");
            }
            catch (ObjectNotFoundException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // GET /temperatures
        [HttpGet]
        [Route("temperatures")]
        public List<CityTemperatureDataContract> GetByTemperatures()
        {
            return cityRepo.GetLatestByTemperatureRegistered(1);
        }

        // GET /temperatures/{page}
        [HttpGet]
        [Route("temperatures/{page}")]
        public List<CityTemperatureDataContract> GetByTemperatures(int page)
        {
            return cityRepo.GetLatestByTemperatureRegistered(page);
        }

        // POST /cities/by_cep/{cep}
        [HttpPost]
        [Route("cities/by_cep/{cep}")]
        public IHttpActionResult PostByCep(string cep)
        {
            try
            {
                var name = CepHelper.GetCityName(cep);

                if (string.IsNullOrEmpty(name))
                {
                    return NotFound();
                }

                cityRepo.Add(new City(name));
                return Ok($"'{name}' was successfully added.");
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
