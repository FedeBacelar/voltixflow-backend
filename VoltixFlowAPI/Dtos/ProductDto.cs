namespace VoltixFlowAPI.Dtos {
	public class ProductDto {
		public int Id { get; set; }
		public string InternalCode { get; set; } = null!;
		public string Name { get; set; } = null!;
		public string Description { get; set; } = null!;
		public ProductCategoryDto Category { get; set; } = null!;
		public decimal EstimatedCostPrice { get; set; }
		public int CurrentStock { get; set; }
		public int StockAlert { get; set; }
		public bool Status { get; set; }
		public string? Observations { get; set; }
	}
}
