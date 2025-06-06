using System.ComponentModel.DataAnnotations;

namespace VoltixFlowAPI.Dtos {
	public class DeliveryNoteItemDto {
		[Required] public int ProductId { get; set; }
		[Required, Range(1, int.MaxValue)] public int Quantity { get; set; }
	}
}
