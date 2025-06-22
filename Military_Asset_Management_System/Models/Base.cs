using System.ComponentModel.DataAnnotations;

namespace Military_Asset_Management_System.Models
{
    public class Base
    {
        [Key]
        public int BaseId { get; set; }

        [Required]
        public string BaseName { get; set; }

        public string Location { get; set; }
    }
}

