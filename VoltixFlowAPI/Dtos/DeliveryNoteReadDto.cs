namespace VoltixFlowAPI.Dtos {
	public class DeliveryNoteReadDto {
		public int Id { get; set; }
		public ClientMiniDto Client { get; set; } = new();
		public DeliveryNoteStatusDto Status { get; set; } = new();
		public DateTime CreatedAt { get; set; }
		public DateTime? DeliveredAt { get; set; }
		public string Observations { get; set; } = string.Empty;
		public List<DeliveryNoteItemReadDto> Items { get; set; } = new();
	}
}
