using VoltixFlowAPI.Dtos;

namespace VoltixFlowAPI.Services.Interfaces {
	public interface IAuthService {
		Task<LoginResponseDto> LoginAsync(string username, string password);
	}
}
