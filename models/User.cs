using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS212FinalProject.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(25)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(25), RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(10), RegularExpression(@"^\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$")]
        public decimal PhoneNumber { get; set; }

        [Required]
        [StringLength(10), RegularExpression(@"^(Customer|ServiceProvider|Receptionist|Manager)$")]
        public string Role { get; set; } = string.Empty;
    }
}
