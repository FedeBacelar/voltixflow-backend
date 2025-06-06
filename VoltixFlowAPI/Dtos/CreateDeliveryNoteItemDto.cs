using System.ComponentModel.DataAnnotations;

namespace VoltixFlowAPI.Dtos {
	public class CreateDeliveryNoteItemDto {
		[Required]
		public int ProductId { get; set; }

		[Required]
		public int Quantity { get; set; }
	}
}
