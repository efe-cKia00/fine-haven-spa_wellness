using System.ComponentModel.DataAnnotations;

namespace CS212FinalProject.Models
{
    public class Appointment
    {
        // Unique identifier for the appointment
        [Key]
        public int Id { get; set; }

        // Service provider may be assigned later; keep nullable.
        public int? ServiceProviderId { get; set; }
        public User? ServiceProvider { get; set; }

        // Customer is required
        [Required]
        public int CustomerId { get; set; }
        public User Customer { get; set; } = null!;

        // Service being booked (required)
        [Required]
        public int ServiceId { get; set; }
        public Service Service { get; set; } = null!;

        // Date and time of the appointment
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateAndTime { get; set; }

        // Status of the appointment (stored as enum; configured in DbContext)
        [Required]
        public StatusType Status { get; set; } = StatusType.Pending;
    }
}
