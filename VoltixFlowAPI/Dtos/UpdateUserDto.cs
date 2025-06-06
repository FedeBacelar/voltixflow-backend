using System.ComponentModel.DataAnnotations;

namespace VoltixFlowAPI.Dtos {
	public class UpdateUserDto {
		[Required, MaxLength(100)]
		public string Username { get; set; }

		[MinLength(6)]
		public string? Password { get; set; }

		[Required]
		public int RoleId { get; set; }

		[Required]
		public bool Active { get; set; }
	}
}
