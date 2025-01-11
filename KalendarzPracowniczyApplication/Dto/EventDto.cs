using System.ComponentModel.DataAnnotations;

namespace KalendarzPracowniczyApplication.Dto
{
    public class EventDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Miejsce wyjazdu jest wymagane")]
        [StringLength(20, ErrorMessage = "Miejsce wyjazdu nie może przekraczać 20 znaków")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Opis jest wymagane")]
        [StringLength(120, ErrorMessage = "Opis nie może przekraczać 120 znaków")]
        public string? Description { get; set; }

        public string? Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsDeleted { get; set; }
        public string? DeletedById { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedTime { get; set; }
        public Guid? CarId { get; set; }

        public CarDto? Car { get; set; }
        public string? UserId { get; set; }
        public UserDto? User { get; set; }
    }
}