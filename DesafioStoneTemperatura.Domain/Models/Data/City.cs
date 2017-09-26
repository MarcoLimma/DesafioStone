using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DesafioStoneTemperatura.Domain.Models.Data
{
    public class City
    {
        public City()
        {
           this.Id = Guid.NewGuid();
        }

        public City(string name) : this()
        {
            this.Name = name;
        }

        public City(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual List<Temperature> Temperatures { get; set; }
    }
}