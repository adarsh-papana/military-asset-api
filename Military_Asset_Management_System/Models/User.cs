using System.ComponentModel.DataAnnotations;

namespace Military_Asset_Management_System.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; } // Changed from PasswordHash

        [Required]
        public string Role { get; set; }  // Admin, BaseCommander, LogisticsOfficer

        public int? BaseId { get; set; }  // Only for BaseCommanders
    }
}
