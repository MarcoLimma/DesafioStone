using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

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

                //ToDo: Testar a velocidade dos conversores de json
                JavaScriptSerializer j = new JavaScriptSerializer();
                var responseObject = j.Deserialize<Dictionary<string, string>>(responseFromServer);

                return responseObject["localidade"] ?? "";
            }
            catch (Exception e)
            {
                throw new Exception("Error on access the weather api. Exception: " + e);
            }
        }
    }
}