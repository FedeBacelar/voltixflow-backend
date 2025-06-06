using VoltixFlowAPI.Models;

namespace VoltixFlowAPI.Repositories.Interfaces {
	public interface IUserRepository {
		Task<User> CreateAsync(User user);
		Task<User?> GetByIdAsync(int id);
		Task<User?> FindByUsernameAsync(string username);
		Task<IEnumerable<User>> GetAllAsync(string? name, int? roleId, int pageNumber, int pageSize);
		Task<User> UpdateAsync(int id, User updatedUser);
		Task<bool> DeleteAsync(int id);
	}
}
