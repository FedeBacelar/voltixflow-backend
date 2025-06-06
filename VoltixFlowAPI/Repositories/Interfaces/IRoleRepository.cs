using VoltixFlowAPI.Models;

namespace VoltixFlowAPI.Repositories.Interfaces {
	public interface IRoleRepository {
		Task<Role> CreateAsync(Role role);
		Task<Role?> GetByIdAsync(int id);
		Task<Role?> FindByNameAsync(string name);
		Task<IEnumerable<Role>> GetAllAsync(int pageNumber, int pageSize);
		Task<Role> UpdateAsync(int id, Role updatedRole);
		Task DeleteAsync(int id);
	}
}
