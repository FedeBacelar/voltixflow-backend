using System.ComponentModel.DataAnnotations;

namespace VoltixFlowAPI.Dtos {
	public class CreateUserDto {
		[Required, MaxLength(100)]
		public string Username { get; set; }

		[Required, MinLength(6)]
		public string Password { get; set; }

		[Required]
		public int RoleId { get; set; }
	}
}
