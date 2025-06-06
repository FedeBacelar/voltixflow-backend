using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoltixFlowAPI.Models
{
    public class DeliveryNoteItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("DeliveryNote")]
        public int DeliveryNoteId { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public DateTime CreatedAt { get; set; }

        public DeliveryNote DeliveryNote { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
