using KalendarzPracowniczyDomain.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace KalendarzPracowniczyDomain.Entities.UserDayOff
{
    public class DayOff
    {
        [Key]
        public Guid Id { get; set; }

        public DateOnly DateDayOff { get; set; }
        public List<User> Users { get; set; }
        public string UserId { get; set; }
    }
}