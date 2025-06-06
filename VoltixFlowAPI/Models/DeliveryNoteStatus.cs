using System.ComponentModel.DataAnnotations;

namespace VoltixFlowAPI.Models
{
    public class DeliveryNoteStatus
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Status { get; set; }

        public ICollection<DeliveryNote> DeliveryNotes { get; set; } = new List<DeliveryNote>();
    }
}
