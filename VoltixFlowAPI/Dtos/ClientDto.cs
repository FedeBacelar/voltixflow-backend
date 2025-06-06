namespace VoltixFlowAPI.Dtos {
	public class ClientDto {
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public ClientTypeDto ClientType { get; set; } = null!;
		public string Cuit { get; set; } = null!;
		public string Phone { get; set; } = null!;
		public string Contact { get; set; } = null!;
		public string Email { get; set; } = null!;
		public string Address { get; set; } = null!;
		public bool Status { get; set; }
		public string? Observations { get; set; }
	}
}
