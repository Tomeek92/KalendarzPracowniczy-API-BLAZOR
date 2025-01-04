using KalendarzPracowniczyDomain.Entities.Users;

namespace KalendarzPracowniczyApplication.Dto
{
    public class DayOffDto
    {
        public Guid Id { get; set; }
        public DateOnly DateDayOff { get; set; }
        public List<UserDto> Users { get; set; }
        public string? UserId { get; set; }
    }
}