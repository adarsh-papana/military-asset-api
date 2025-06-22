using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Military_Asset_Management_System.Models
{
    public class Purchase
    {
        [Key]
        public int PurchaseId { get; set; }

        [Required]
        public int BaseId { get; set; }
        [ForeignKey("BaseId")]
        public Base Base { get; set; }

        [Required]
        public int EquipmentTypeId { get; set; }
        [ForeignKey("EquipmentTypeId")]
        public EquipmentType EquipmentType { get; set; }

        [Required]
        public int Quantity { get; set; }

        public DateTime PurchaseDate { get; set; } = DateTime.Now;
    }
}
