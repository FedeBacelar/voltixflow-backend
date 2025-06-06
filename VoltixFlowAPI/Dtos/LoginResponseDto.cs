namespace VoltixFlowAPI.Dtos {
	public class LoginResponseDto {
		public string Token { get; set; } = string.Empty;
		public DateTime ExpiresAt { get; set; }
		public int UserId { get; set; }
		public string Username { get; set; } = string.Empty;
		public string Role { get; set; } = string.Empty;
		public IEnumerable<string> Permissions { get; set; } = Array.Empty<string>();
	}
}
