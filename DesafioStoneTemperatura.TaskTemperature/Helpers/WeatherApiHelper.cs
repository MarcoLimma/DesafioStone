using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using DesafioStoneTemperatura.Domain.Models;

namespace DesafioStoneTemperatura.TaskTemperature.Helpers
{
    public class WeatherApiHelper
    {
        public Temperature GetTemperature(City city)
        {
            try
            {
                string url = String.Format("https://api.hgbrasil.com/weather/?format=json&city_name={0}&key=0646d698", city.Name);

                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                Console.WriteLine(((HttpWebResponse) response).StatusDescription);
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                    
                //ToDo: Testar a velocidade dos conversores de json
                JavaScriptSerializer j = new JavaScriptSerializer();
                var responseObject = j.Deserialize<Dictionary<string, object>>(responseFromServer);

                //ToDo: Tratar o caso de nao ter a cidade na API de clima
                var results = responseObject["results"];

                var temperature = ((Dictionary<string, object>)results)["temp"];

                Console.WriteLine(responseFromServer);
                reader.Close();
                response.Close();

                return new Temperature((int)temperature, city.Id);

            }
            catch (Exception e)
            {
                throw new Exception("Error on access the weather api. Exception: " + e);
            }
        }
    }
}
