using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using DesafioStoneTemperatura.Domain.Models.Data;
using Newtonsoft.Json;

namespace DesafioStoneTemperatura.TaskTemperature.Helpers
{
    public static class WeatherApiHelper
    {
        public static Temperature GetTemperature(City city)
        {
            try
            {
                string url = String.Format("https://api.hgbrasil.com/weather/?format=json&city_name={0}&key=0646d698", city.Name);

                var request = WebRequest.Create(url);
                var response = request.GetResponse();
                
                var dataStream = response.GetResponseStream();
                var reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                    
                var responseObject = JsonConvert.DeserializeObject<ResponseObject>(responseFromServer);
                var results = responseObject.results;
                var temperature = results.temp;

                reader.Close();
                response.Close();

                return new Temperature(temperature, city.Id);

            }
            catch (Exception e)
            {
                throw new Exception("Error accessing the weather api. Exception: " + e);
            }
        }

        private class Forecast
        {
            public string date { get; set; }
            public string weekday { get; set; }
            public string max { get; set; }
            public string min { get; set; }
            public string description { get; set; }
            public string condition { get; set; }
        }

        private class Results
        {
            public int temp { get; set; }
            public string date { get; set; }
            public string time { get; set; }
            public string condition_code { get; set; }
            public string description { get; set; }
            public string currently { get; set; }
            public string cid { get; set; }
            public string city { get; set; }
            public string img_id { get; set; }
            public string humidity { get; set; }
            public string wind_speedy { get; set; }
            public string sunrise { get; set; }
            public string sunset { get; set; }
            public string condition_slug { get; set; }
            public string city_name { get; set; }
            public List<Forecast> forecast { get; set; }
        }

        private class ResponseObject
        {
            public string by { get; set; }
            public bool valid_key { get; set; }
            public Results results { get; set; }
            public double execution_time { get; set; }
            public bool from_cache { get; set; }
        }
    }
}
