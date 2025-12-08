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
        [StringLength(25)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public decimal PhoneNumber { get; set; }

        [Required]
        [StringLength(10)]
        public string Role { get; set; }
    }
}
