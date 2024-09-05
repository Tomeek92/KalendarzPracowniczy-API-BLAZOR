namespace KalendarzPracowniczyApplication.CQRS.Commands.Users.LogIn
{
    public class Result
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}