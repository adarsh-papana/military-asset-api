using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Military_Asset_Management_System.Models
{
    public class Asset
    {
        [Key]
        public int AssetId { get; set; }

        [Required]
        public int EquipmentTypeId { get; set; }
        [ForeignKey("EquipmentTypeId")]
        public EquipmentType EquipmentType { get; set; }

        [Required]
        public int BaseId { get; set; }
        [ForeignKey("BaseId")]
        public Base Base { get; set; }

        public int OpeningBalance { get; set; }
        public int ClosingBalance { get; set; }
    }
}
