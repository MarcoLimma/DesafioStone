using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace DesafioStoneTemperatura.Helpers
{
    public class CepHelper
    {
        public string GetCityName(string cep)
        {
            try
            {
                string url = String.Format("https://viacep.com.br/ws/{0}/json/", cep);

                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                var responseObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseFromServer);

                return responseObject["localidade"] ?? "";
            }
            catch (Exception e)
            {
                throw new Exception("Error on access the weather api. Exception: " + e);
            }
        }
    }
}