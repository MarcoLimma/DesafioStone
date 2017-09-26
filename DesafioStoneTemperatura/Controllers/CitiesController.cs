using System.Web.Http;
using DesafioStoneTemperatura.Data;
using DesafioStoneTemperatura.Data.Repositories;
using DesafioStoneTemperatura.Domain.Models.Data;
using DesafioStoneTemperatura.Helpers;
using DesafioStoneTemperatura.Domain.Models.Api;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Net;
using System.Web.Http.Results;
using DesafioStoneTemperatura.Domain.Models.Data.Interfaces;

namespace DesafioStoneTemperatura.Controllers
{
    public class CitiesController : ApiController
    {
        private DataContext context;
        private ICityRepository cityRepo;

        public CitiesController()
        {
            this.context = new DataContext();
            this.cityRepo = new CityRepository(context);
        }


        public CitiesController(ICityRepository _cityRepo)
        {
            this.context = new DataContext();
            this.cityRepo = _cityRepo;
        }

        [HttpGet]
        [Route("cities/{name}/temperatures")]
        // GET /cities/{name}/temperature
        public CityDataContract Get(string name)
        {
            return cityRepo.GetTemperatures(name, 30);
        }

        [HttpPost]
        [Route("cities/{name}")]
        // POST /cities/{name}
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

        [HttpDelete]
        [Route("cities/{name}")]
        // DELETE /cities/{name}
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

        [HttpDelete]
        [Route("cities/{name}/temperatures")]
        // DELETE /cities/{city_name}/temperatures
        public BasicResponse DeleteTemperatures(string name)
        {
            try
            {
                cityRepo.DeleteTemperatures(name);
                return new BasicResponse(Status.Success,
                    $"The temperature history of '{name}' was successfully deleted.");
            }
            catch (Exception e)
            {
                return new BasicResponse(Status.Error, e.ToString());
            } 
        }

        [HttpGet]
        [Route("temperatures")]
        // GET /cities
        public List<CityTemperatureDataContract> GetByTemperatures()
        {
            return cityRepo.GetLatestByTemperatureRegistered(1);
        }

        [HttpGet]
        [Route("temperatures/{page}")]
        // GET /cities
        public List<CityTemperatureDataContract> GetByTemperatures(int page)
        {
            return cityRepo.GetLatestByTemperatureRegistered(page);
        }

        [HttpPost]
        [Route("cities/by_cep/{cep}")]
        // POST /cities/by_cep/{cep}
        public BasicResponse PostByCep(string cep)
        {
            try
            {
                string name = new CepHelper().GetCityName(cep);
                cityRepo.Add(new City(name));
                return new BasicResponse(Status.Success, $"'{name}' was successfully added.");
            }
            catch (Exception e)
            {
                return new BasicResponse(Status.Error, e.ToString());
            }
        }
    }
}
