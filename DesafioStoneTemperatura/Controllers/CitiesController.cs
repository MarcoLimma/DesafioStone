using System.Web.Http;
using DesafioStoneTemperatura.Data;
using DesafioStoneTemperatura.Data.Repositories;
using DesafioStoneTemperatura.Domain.Models.Data;
using DesafioStoneTemperatura.Helpers;
using DesafioStoneTemperatura.Domain.Models.Api;
using System;

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
            return cityRepo.GetTemperatures(name, 30);
        }

        [HttpPost]
        [Route("cities/{name}")]
        // POST /cities/{name}
        public BasicResponse Post(string name)
        {
            try
            {
                cityRepo.Add(new City(name));
                return new BasicResponse(Status.Success, String.Format("'{0}' was successfully added.", name));
            }
            catch (Exception e)
            {
                return new BasicResponse(Status.Error, e.ToString());
            }
            
        }

        [HttpDelete]
        [Route("cities/{name}")]
        // DELETE /cities/{name}
        public BasicResponse Delete(string name)
        {
            try
            {
                cityRepo.Remove(name);
                return new BasicResponse(Status.Success, String.Format("'{0}' was successfully deleted.", name));
            }
            catch (Exception e)
            {
                return new BasicResponse(Status.Error, e.ToString());
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
                return new BasicResponse(Status.Success, String.Format("The temperature history of '{0}' was successfully deleted.", name));
            }
            catch (Exception e)
            {
                return new BasicResponse(Status.Error, e.ToString());
            } 
        }

        [HttpGet]
        [Route("temperatures")]
        // GET /cities
        public object GetByTemperatures()
        {
            return cityRepo.GetLatestByTemperatureRegistered(1);
        }

        [HttpGet]
        [Route("temperatures/{page}")]
        // GET /cities
        public object GetByTemperatures(int page)
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
                return new BasicResponse(Status.Success, String.Format("'{0}' was successfully added.", name));
            }
            catch (Exception e)
            {
                return new BasicResponse(Status.Error, e.ToString());
            }
        }
    }
}
