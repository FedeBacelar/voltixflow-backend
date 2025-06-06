using System.ComponentModel.DataAnnotations;

namespace VoltixFlowAPI.Dtos {
	public class UpdateDeliveryNoteItemDto {
		[Required]
		public int Id { get; set; }

		[Required]
		public int ProductId { get; set; }

		[Required]
		public int Quantity { get; set; }
	}
}
