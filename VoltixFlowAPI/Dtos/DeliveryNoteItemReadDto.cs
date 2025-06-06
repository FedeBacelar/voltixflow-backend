namespace VoltixFlowAPI.Dtos {
	public class DeliveryNoteItemReadDto {
		public int ProductId { get; set; }
		public string ProductCode { get; set; } = string.Empty;
		public string ProductName { get; set; } = string.Empty;
		public int Quantity { get; set; }
	}
}
