using DesafioStoneTemperatura.Domain.Models.Data;
using DesafioStoneTemperatura.Domain.Models.Data.Interfaces;

namespace DesafioStoneTemperatura.Data.Repositories
{
    public class TemperatureRepository : ITemperatureRepository
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


