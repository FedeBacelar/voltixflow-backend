using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoltixFlowAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public required string InternalCode { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(500)]
        public required string Description { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [Required]
        public decimal EstimatedCostPrice { get; set; }

        [Required]
        public int CurrentStock { get; set; }

        [Required]
        public int StockAlert { get; set; }

        public bool Status { get; set; }

        [MaxLength(500)]
        public required string Observations { get; set; }

        public ProductCategory Category { get; set; } = null!;
        public ICollection<DeliveryNoteItem> DeliveryNoteItems { get; set; } = new List<DeliveryNoteItem>();
    }
}
