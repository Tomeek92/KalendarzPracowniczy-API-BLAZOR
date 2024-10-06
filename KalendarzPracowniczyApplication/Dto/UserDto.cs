using KalendarzPracowniczyDomain.Entities.Works;
using System.ComponentModel.DataAnnotations;

namespace KalendarzPracowniczyApplication.Dto
{
    public class UserDto
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Nazwa użytkownika jest wymagane")]
        [StringLength(50, ErrorMessage = "Nazwa użytkownika nie może przekraczać 50 znaków")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Imię jest wymagane")]
        [StringLength(50, ErrorMessage = "Imię nie może przekraczać 50 znaków")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        [StringLength(50, ErrorMessage = "Nazwisko nie może przekraczać 50 znaków")]
        public string Surname { get; set; } = null!;

        [Required(ErrorMessage = "E-mail jest wymagany")]
        [EmailAddress(ErrorMessage = "Niepoprawny format adresu email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Hasło musi zawierać od 6 do 100 znaków")]
        public string Password { get; set; } = null!;

        public ICollection<EventDto>? Events { get; set; }
        public List<Work>? Works { get; set; }
    }
}