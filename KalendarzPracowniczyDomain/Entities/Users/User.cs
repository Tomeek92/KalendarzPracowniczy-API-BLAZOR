using KalendarzPracowniczyDomain.Entities.UserDayOff;
using KalendarzPracowniczyDomain.Entities.Events;
using KalendarzPracowniczyDomain.Entities.Works;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace KalendarzPracowniczyDomain.Entities.Users
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }

        [NotMapped]
        public string? Password { get; set; }

        public ICollection<Event>? Events { get; set; }
        public List<Work>? Works { get; set; }
        public ICollection<DayOff> DaysOff { get; set; }
    }
}