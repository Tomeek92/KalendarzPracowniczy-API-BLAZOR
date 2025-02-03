using KalendarzPracowniczyDomain.Entities.Works;
using System.ComponentModel.DataAnnotations;

namespace KalendarzPracowniczyApplication.Dto
{
    public class UserDto
    {
        public string? Id { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Nazwa użytkownika może zawierać tylko litery (A-Z, a-z) i cyfry (0-9).")]
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagane")]
        [StringLength(50, ErrorMessage = "Nazwa użytkownika nie może przekraczać 50 znaków")]
        public string UserName { get; set; } = null!;

        [StringLength(20, ErrorMessage = "Imię nie może przekraczać 20 znaków")]
        public string? Name { get; set; }

        [StringLength(50, ErrorMessage = "Nazwisko nie może przekraczać 50 znaków")]
        public string? Surname { get; set; }

        [EmailAddress(ErrorMessage = "Niepoprawny format adresu email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Hasło musi zawierać od 6 do 20 znaków")]
        [RegularExpression(@"^(?=.*[^a-zA-Z0-9]).+$",
        ErrorMessage = "Hasło musi zawierać co najmniej jeden znak specjalny")]
        public string Password { get; set; } = null!;

        public ICollection<EventDto>? Events { get; set; }
        public List<Work>? Works { get; set; }
    }
}