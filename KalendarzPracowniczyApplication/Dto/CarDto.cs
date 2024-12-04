using KalendarzPracowniczyDomain.Entities.Events;

namespace KalendarzPracowniczyApplication.Dto
{
    public class CarDto
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }
        public DateTime? Year { get; set; }
        public string? Model { get; set; }
        public string? CarPlatesNumber { get; set; }
        public string? CarKm { get; set; }
        public DateTime? CarInspection { get; set; }

    }
}