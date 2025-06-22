using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Military_Asset_Management_System.Models
{
    public class Assignment
    {
        [Key]
        public int AssignmentId { get; set; }

        [Required]
        public int BaseId { get; set; }
        [ForeignKey("BaseId")]
        public Base Base { get; set; }

        [Required]
        public int EquipmentTypeId { get; set; }
        [ForeignKey("EquipmentTypeId")]
        public EquipmentType EquipmentType { get; set; }

        public string AssignedTo { get; set; }
        public int AssignedQuantity { get; set; }
        public int ExpendedQuantity { get; set; }

        public DateTime AssignedDate { get; set; } = DateTime.Now;
    }
}
