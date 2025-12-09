using System.ComponentModel.DataAnnotations;

namespace CS212FinalProject.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(256)]
        public string Email { get; set; } = string.Empty;

        // Store phone numbers as strings to preserve formatting and international numbers
        [MaxLength(30)]
        public string PhoneNumber { get; set; } = string.Empty;

        // Use the RoleType enum. EF Core will persist this as a string via configuration.
        [Required]
        public RoleType Role { get; set; } = RoleType.Customer;

        // Store only hashed password values
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
    }
}
