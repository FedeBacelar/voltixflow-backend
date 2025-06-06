using System.ComponentModel.DataAnnotations;

namespace VoltixFlowAPI.Dtos {
	public class UpdateDeliveryNoteDto {
		[Required] public int StatusId { get; set; }           
		public string? Observations { get; set; }
		[Required, MinLength(1)]
		public List<DeliveryNoteItemDto> Items { get; set; } = new();
	}
}
