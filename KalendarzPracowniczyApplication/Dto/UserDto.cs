namespace KalendarzPracowniczyApplication.Dto
{
    public class UserDto
    {
        public string? Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? Email { get; set; }
        public string Password { get; set; } = null!;
        public ICollection<EventDto>? Events { get; set; }
    }
}