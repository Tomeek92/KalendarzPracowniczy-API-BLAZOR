using KalendarzPracowniczyDomain.Entities.Cars;

namespace KalendarzPracowniczyDomain.Interfaces
{
    public interface ICarRepository
    {
        Task<Car> GetElementById(Guid id);

        Task Delete(Guid id);

        Task Update(Car carUpdate);

        Task Create(Car newCar);

        Task<IEnumerable<Car>> GetAllWorkers();
    }
}