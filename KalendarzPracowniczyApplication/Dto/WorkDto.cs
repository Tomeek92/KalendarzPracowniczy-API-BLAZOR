using KalendarzPracowniczyDomain.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace KalendarzPracowniczyApplication.Dto
{
    public class WorkDto
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string? Title { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public bool? IsCompleted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
    }
}