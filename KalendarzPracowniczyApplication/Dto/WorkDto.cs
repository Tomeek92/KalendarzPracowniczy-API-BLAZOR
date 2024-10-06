using KalendarzPracowniczyDomain.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace KalendarzPracowniczyApplication.Dto
{
    public class WorkDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Tytuł zadania jest wymagany")]
        [StringLength(100, ErrorMessage = "Tytuł zadania nie może przekraczać 100 znaków")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Opis zadania jest wymagany")]
        [StringLength(500, ErrorMessage = "Opis zadania nie może przekraczać 500 znaków")]
        public string? Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public bool? IsCompleted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
    }
}