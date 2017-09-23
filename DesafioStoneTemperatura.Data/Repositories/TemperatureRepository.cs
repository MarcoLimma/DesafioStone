using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using DesafioStoneTemperatura.Domain.Models;

namespace DesafioStoneTemperatura.Data.Repositories
{
    public class TemperatureRepository
    {
        private DataContext context;

        public TemperatureRepository(DataContext _context)
        {
            this.context = _context;
        }

        public void Add(Temperature temperature)
        {
            context.Temperatures.Add(temperature);
            context.SaveChanges();
        }

        public void Remove(Temperature temperature)
        {
            context.Temperatures.Remove(temperature);
            context.SaveChanges();
        }

    }
}


