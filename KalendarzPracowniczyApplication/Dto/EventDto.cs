﻿namespace KalendarzPracowniczyApplication.Dto
{
    public class EventDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string UserId { get; set; }
        public UserDto User { get; set; } = null!;
    }
}