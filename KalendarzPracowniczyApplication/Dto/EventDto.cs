namespace KalendarzPracowniczyApplication.Dto
{
    public class EventDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        //public User? User { get; set; }
    }
}
