using System;
using System.IO;
using System.Net;
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

                var responseObject = JsonConvert.DeserializeObject<ResponseObject>(responseFromServer);

                return responseObject.localidade ?? "";
            }
            catch (Exception e)
            {
                throw new Exception("Error on access the weather api. Exception: " + e);
            }
        }

        private class ResponseObject
        {
            public string cep { get; set; }
            public string logradouro { get; set; }
            public string complemento { get; set; }
            public string bairro { get; set; }
            public string localidade { get; set; }
            public string uf { get; set; }
            public string unidade { get; set; }
            public string ibge { get; set; }
            public string gia { get; set; }
        }
    }
}