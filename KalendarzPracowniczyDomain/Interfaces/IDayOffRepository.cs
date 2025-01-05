using KalendarzPracowniczyDomain.Entities.UserDayOff;

namespace KalendarzPracowniczyDomain.Interfaces
{
    public interface IDayOffRepository
    {
        Task<DayOff> GetElementById(Guid id);

        Task Delete(Guid id);

        Task Create(DayOff createDayOff, CancellationToken cancellationToken);

        Task Update(DayOff updateDayOff);

        Task<List<DayOff>> GetAllDaysOff();
    }
}