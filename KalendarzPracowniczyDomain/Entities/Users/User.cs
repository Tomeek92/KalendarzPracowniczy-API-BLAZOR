using KalendarzPracowniczyDomain.Entities.Events;
using KalendarzPracowniczyDomain.Entities.Works;
using Microsoft.AspNetCore.Identity;

namespace KalendarzPracowniczyDomain.Entities.Users
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; } 
        public ICollection<Event>? Events { get; set; }
        public List<Work>? Works { get; set; }
    }
}