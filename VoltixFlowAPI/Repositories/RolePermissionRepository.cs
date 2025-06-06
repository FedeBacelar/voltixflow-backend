using Microsoft.EntityFrameworkCore;
using VoltixFlowAPI.Data;
using VoltixFlowAPI.Models;
using VoltixFlowAPI.Repositories.Interfaces;

namespace VoltixFlowAPI.Repositories {
	public class RolePermissionRepository : IRolePermissionRepository {
		private readonly VoltixDbContext _context;
		public RolePermissionRepository(VoltixDbContext context) => _context = context;

		public async Task<IEnumerable<RolePermission>> GetByRoleIdAsync(int roleId) {
			return await _context.RolePermissions
							 .Include(rolePermission => rolePermission.Permission)
							 .Where(rolePermission => rolePermission.RoleId == roleId)
							 .ToListAsync();
		}
	}
}
