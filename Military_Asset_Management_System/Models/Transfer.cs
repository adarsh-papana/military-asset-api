using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Military_Asset_Management_System.Models
{
    public class Transfer
    {
        [Key]
        public int TransferId { get; set; }

        [Required]
        public int FromBaseId { get; set; }
        [ForeignKey("FromBaseId")]
        public Base FromBase { get; set; }

        [Required]
        public int ToBaseId { get; set; }
        [ForeignKey("ToBaseId")]
        public Base ToBase { get; set; }

        [Required]
        public int EquipmentTypeId { get; set; }
        [ForeignKey("EquipmentTypeId")]
        public EquipmentType EquipmentType { get; set; }

        public int Quantity { get; set; }

        public DateTime TransferDate { get; set; } = DateTime.Now;
    }
}
