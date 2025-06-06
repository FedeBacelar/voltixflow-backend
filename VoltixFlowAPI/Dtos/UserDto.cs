namespace VoltixFlowAPI.Dtos {
	public class UserDto {
		public int Id { get; set; }
		public string Username { get; set; } = null!;
		public bool Active { get; set; }
		public RoleDto Role { get; set; } = null!;
	}
}
