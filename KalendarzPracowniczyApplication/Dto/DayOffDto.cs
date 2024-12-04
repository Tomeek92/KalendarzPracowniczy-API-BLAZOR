using KalendarzPracowniczyDomain.Entities.Users;

namespace KalendarzPracowniczyApplication.Dto
{
    public class DayOffDto
    {
        public Guid Id { get; set; }
        public DateOnly DateDayOff { get; set; }
        public ICollection<User> Users { get; set; }
    }
}