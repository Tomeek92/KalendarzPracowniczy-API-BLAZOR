﻿using KalendarzPracowniczyDomain.Entities.Events;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace KalendarzPracowniczyDomain.Entities.Cars
{
    public class Car
    {
        [Key]
        public Guid Id { get; set; }

        public bool? IsActive { get; set; } = true;
        public string? Name { get; set; }
        public DateTime? Production { get; set; }
        public string? Model { get; set; }
        public string? CarPlatesNumber { get; set; }
        public string? CarKm { get; set; }
        public DateOnly? CarInspection { get; set; }
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}