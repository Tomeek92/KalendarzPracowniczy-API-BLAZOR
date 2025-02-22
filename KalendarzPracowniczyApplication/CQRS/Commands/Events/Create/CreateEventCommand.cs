﻿using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Events.Create
{
    public class CreateEventCommand : IRequest<EventDto>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Guid? CarId { get; set; }
        public string? Name { get; set; }
        public DateTime StartDate { get; set; }
        public string? UserId { get; set; }
    }
}