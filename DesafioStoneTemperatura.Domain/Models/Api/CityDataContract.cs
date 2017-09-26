using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioStoneTemperatura.Domain.Models.Api
{
    public class CityDataContract
    {
        public string city { get; set; }
        public List<TemperatureDataContract> temperatures { get; set; }
    }
}
