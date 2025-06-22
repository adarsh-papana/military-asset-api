using System.ComponentModel.DataAnnotations;

namespace Military_Asset_Management_System.Models
{
    public class EquipmentType
    {
        [Key]
        public int EquipmentTypeId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
