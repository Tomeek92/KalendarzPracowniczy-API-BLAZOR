using System.ComponentModel.DataAnnotations;

namespace KalendarzPracowniczyDomain.Entities.Workers
{
    public class Worker
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? Email { get; set; }


    }
}
