using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoltixFlowAPI.Models
{
    public class DeliveryNote
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Client")]
        public int ClientId { get; set; }

        [Required]
        [ForeignKey("Status")]
        public int StatusId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }

        [MaxLength(500)]
        public required string Observations { get; set; }

        [Required]
        [ForeignKey("CreatedByUser")]
        public int CreatedByUserId { get; set; }

        public Client Client { get; set; } = null!;
        public DeliveryNoteStatus Status { get; set; } = null!;
        public User CreatedByUser { get; set; } = null!;

        public ICollection<DeliveryNoteItem> Items { get; set; } = new List<DeliveryNoteItem>();
    }
}
