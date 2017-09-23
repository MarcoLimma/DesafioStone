using System;
using Newtonsoft.Json;

namespace DesafioStoneTemperatura.Domain.Models
{
    public class Temperature
    {

        public Temperature()
        {
            this.Id = Guid.NewGuid(); 
        }

        public Temperature(int value, Guid cityId) : this()
        {
            this.Value = value;
            this.CityId = cityId;
            this.Date = DateTime.Now;
        }

        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public int Value { get; set; }

        [JsonIgnore]
        public virtual City City { get; set; }

        public Guid CityId { get; set; }
    }
}