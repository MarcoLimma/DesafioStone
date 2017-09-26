using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace DesafioStoneTemperatura.Helpers
{
    public static class CepHelper
    {
        public static string GetCityName(string cep)
        {
            try
            {
                string url = String.Format("https://viacep.com.br/ws/{0}/json/", cep);

                var request = WebRequest.Create(url);
                var response = request.GetResponse();
                var dataStream = response.GetResponseStream();
                var reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                var responseObject = JsonConvert.DeserializeObject<ResponseObject>(responseFromServer);

                return responseObject.localidade ?? "";
            }
            catch (Exception e)
            {
                throw new Exception("Error accessing the weather api. Exception: " + e);
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