namespace DesafioStoneTemperatura.Domain.Models.Data.Interfaces
{
    public interface ITemperatureRepository
    {
        void Add(Temperature temperature);
        void Remove(Temperature temperature);
    }
}