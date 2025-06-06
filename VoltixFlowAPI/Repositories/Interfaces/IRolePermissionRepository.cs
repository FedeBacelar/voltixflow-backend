using VoltixFlowAPI.Models;

namespace VoltixFlowAPI.Repositories.Interfaces {
	public interface IRolePermissionRepository {
		Task<IEnumerable<RolePermission>> GetByRoleIdAsync(int roleId);
	}
}
