using KalendarzPracowniczyDomain.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace KalendarzPracowniczyDomain.Entities.UserDayOff
{
    public class DayOff
    {
        [Key]
        public Guid Id { get; set; }

        public DateOnly DateDayOff { get; set; }
        public ICollection<User> Users { get; set; }
    }
}