using KalendarzPracowniczyDomain.Entities.Events;
using System.ComponentModel.DataAnnotations;

namespace KalendarzPracowniczyDomain.Entities.Users
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string? Email { get; set; }
        public string PasswordHash { get; set; } = null!;
        public ICollection<Event> Events { get; set; } = null!;
    }
}
