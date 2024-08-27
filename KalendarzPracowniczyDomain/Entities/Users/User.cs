using KalendarzPracowniczyDomain.Entities.Events;
using Microsoft.AspNetCore.Identity;

namespace KalendarzPracowniczyDomain.Entities.Users
{
    public class User : IdentityUser
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public ICollection<Event> Events { get; set; } = null!;
    }
}