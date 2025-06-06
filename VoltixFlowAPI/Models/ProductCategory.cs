using System.ComponentModel.DataAnnotations;

namespace VoltixFlowAPI.Models
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(50)]
        public required string GoogleIcon { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
