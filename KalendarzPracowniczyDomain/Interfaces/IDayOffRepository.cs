using KalendarzPracowniczyDomain.Entities.UserDayOff;

namespace KalendarzPracowniczyDomain.Interfaces
{
    public interface IDayOffRepository
    {
        Task<List<DayOff>> GetElementById(string UserId);

        Task Delete(Guid id);

        Task Create(DayOff createDayOff, CancellationToken cancellationToken);

        Task Update(DayOff updateDayOff);

        Task<List<DayOff>> GetAllDaysOff();
    }
}