using System.ComponentModel.DataAnnotations;

namespace KalendarzPracowniczyApplication.Dto
{
    public class EventDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Miejsce wyjazdu jest wymagane")]
        [StringLength(100, ErrorMessage = "Miejsce wyjazdu nie może przekraczać 100 znaków")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Opis jest wymagane")]
        [StringLength(500, ErrorMessage = "Opis nie może przekraczać 500 znaków")]
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedTime { get; set; }
        public string? Car { get; set; }
        public string? UserId { get; set; }
        public UserDto? User { get; set; }
    }
}