using System.Collections.Generic;

namespace DesafioStoneTemperatura.Domain.Models.Api
{
    public class CityDataContract
    {
        public string city { get; set; }
        public List<TemperatureDataContract> temperatures { get; set; }
    }
}
