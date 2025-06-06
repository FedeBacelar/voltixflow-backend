using System;
using System.Collections.Generic;

namespace VoltixFlowAPI.Dtos {
	public class DeliveryNoteDto {
		public int Id { get; set; }

		public int ClientId { get; set; }
		public string ClientName { get; set; } = null!;

		public DeliveryNoteStatusDto Status { get; set; } = null!;

		public DateTime CreatedAt { get; set; }
		public DateTime? DeliveredAt { get; set; }

		public string? Observations { get; set; }
		public int CreatedByUserId { get; set; }

		public List<DeliveryNoteItemDto> Items { get; set; } = new();
	}
}
