namespace KalendarzPracowniczyApplication.Dto
{
    public class UserDto
    {
        public string Username { get; set; } = null!;
        public string? Email { get; set; }
        public string PasswordHash { get; set; } = null!;
        public ICollection<EventDto> Events { get; set; } = null!;
    }
}