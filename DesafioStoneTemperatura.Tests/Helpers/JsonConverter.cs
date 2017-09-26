using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace DesafioStoneTemperatura.Tests.Helpers
{
    [TestClass()]
    public class JsonConverter
    {
        private string responseFromServer;

        [TestInitialize]
        public void ApiInit()
        {
            string cityName = "Rio de Janeiro";
            string url = String.Format("https://api.hgbrasil.com/weather/?format=json&city_name={0}&key=0646d698", cityName);

            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            responseFromServer = reader.ReadToEnd();

        }

        [TestMethod()]
        public void Converter1()
        {
            var responseObject = JsonConverter1(responseFromServer);
            //testar a velocidade em que o teste foi executado

        }

        [TestMethod()]
        public void Converter2()
        {
            var responseObject = JsonConverter2(responseFromServer);
            //testar a velocidade em que o teste foi executado
        }


        public Dictionary<string, object> JsonConverter1(string json)
        {
            return new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(json);
        }

        public Dictionary<string, object> JsonConverter2(string json)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        }
    }
}