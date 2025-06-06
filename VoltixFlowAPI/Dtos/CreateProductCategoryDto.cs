using System.ComponentModel.DataAnnotations;

namespace VoltixFlowAPI.Dtos {
	public class CreateProductCategoryDto {
		[Required, MaxLength(100)]
		public string Name { get; set; }

		[Required, MaxLength(50)]
		public string GoogleIcon { get; set; }
	}
}
