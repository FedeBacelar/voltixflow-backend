using System.ComponentModel.DataAnnotations;

namespace VoltixFlowAPI.Dtos {
	public class UpdateClientDto {
		[Required, MaxLength(100)]
		public string Name { get; set; }

		[Required]
		public int ClientTypeId { get; set; }

		[Required, MaxLength(20)]
		public string Cuit { get; set; }

		[Required, MaxLength(20)]
		public string Phone { get; set; }

		[Required, MaxLength(100)]
		public string Contact { get; set; }

		[Required, EmailAddress, MaxLength(100)]
		public string Email { get; set; }

		[Required, MaxLength(200)]
		public string Address { get; set; }

		[Required]
		public bool Status { get; set; }

		[MaxLength(500)]
		public string? Observations { get; set; }
	}
}
