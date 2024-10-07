using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace KalendarzPracowniczyDomain.Entities.Cars
{
    public class Car
    {
        [Key]
        public Guid Id { get; set; }

        public string? Name { get; set; }
        public DateTime? Year { get; set; }
        public string? Model { get; set; }
        public string? CarPlatesNumber { get; set; }
    }
}