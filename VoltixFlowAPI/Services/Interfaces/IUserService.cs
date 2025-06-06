using VoltixFlowAPI.Models;
using VoltixFlowAPI.Dtos;

namespace VoltixFlowAPI.Services.Interfaces {
	public interface IUserService {
		Task<User> CreateUserAsync(CreateUserDto dto);
		Task<User?> GetUserByIdAsync(int id);
		Task<User?> GetByUsernameAsync(string username);
		Task<IEnumerable<User>> GetUsersAsync(string? name, int? roleId, int pageNumber, int pageSize);
		Task<User> UpdateUserAsync(int id, UpdateUserDto dto);
		Task<bool> DeleteUserAsync(int id);
	}
}
