using System.ComponentModel.DataAnnotations;

namespace VoltixFlowAPI.Dtos {
	public class CreateProductDto {
		[Required, MaxLength(50)]
		public string InternalCode { get; set; }

		[Required, MaxLength(100)]
		public string Name { get; set; }

		[Required, MaxLength(500)]
		public string Description { get; set; }

		[Required]
		public int CategoryId { get; set; }

		[Required]
		public decimal EstimatedCostPrice { get; set; }

		[Required]
		public int CurrentStock { get; set; }

		[Required]
		public int StockAlert { get; set; }

		public bool Status { get; set; } = true;

		[MaxLength(500)]
		public string? Observations { get; set; }
	}
}
