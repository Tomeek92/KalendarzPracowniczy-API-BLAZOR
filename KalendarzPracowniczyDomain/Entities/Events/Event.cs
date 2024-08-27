using KalendarzPracowniczyDomain.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace KalendarzPracowniczyDomain.Entities.Events
{
    public class Event
    {
        [Key]
        public Guid Id { get; set; }

        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}