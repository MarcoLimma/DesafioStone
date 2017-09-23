using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using DesafioStoneTemperatura.Domain.Models;

namespace DesafioStoneTemperatura.TaskTemperature
{
    public class hgbrasil
    {
        public Temperature GetTemperature(City city)
        {
            try
            {
                string url = "https://api.hgbrasil.com/weather/?format=json&city_name=" + city.Name;

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

                var temp = ((Dictionary<string, object>)results)["temp"];

                Console.WriteLine(responseFromServer);
                reader.Close();
                response.Close();

                return new Temperature((int)temp, city.Id);

            }
            catch (Exception e)
            {
                throw new Exception("Error on access the weather api. Exception: " + e);
            }
        }
    }
}
