using Microsoft.EntityFrameworkCore;
using VoltixFlowAPI.Data;
using VoltixFlowAPI.Models;
using VoltixFlowAPI.Repositories.Interfaces;

namespace VoltixFlowAPI.Repositories {
	public class RoleRepository : IRoleRepository {
		private readonly VoltixDbContext _context;

		public RoleRepository(VoltixDbContext context) => _context = context;

		public async Task<Role> CreateAsync(Role role) {
			_context.Roles.Add(role);
			await _context.SaveChangesAsync();
			return role;
		}

		public async Task<Role?> GetByIdAsync(int id) {
			return await _context.Roles
				.Include(role => role.Users)
				.FirstOrDefaultAsync(role => role.Id == id);
		}

		public async Task<Role?> FindByNameAsync(string name) {
			return await _context.Roles
				.Include(role => role.Users)
				.FirstOrDefaultAsync(role => role.Name == name);
		}

		public async Task<IEnumerable<Role>> GetAllAsync(int pageNumber, int pageSize) {
			return await _context.Roles
				.Include(role => role.Users)
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();
		}

		public async Task<Role> UpdateAsync(int id, Role updatedRole) {
			var role = await _context.Roles.FindAsync(id);
			if (role == null) throw new KeyNotFoundException("Role not found");

			role.Name = updatedRole.Name;
			await _context.SaveChangesAsync();
			return role;
		}

		public async Task DeleteAsync(int id) {
			var role = await _context.Roles.FindAsync(id);
			if (role == null) throw new KeyNotFoundException("Role not found");

			_context.Roles.Remove(role);
			await _context.SaveChangesAsync();
		}
	}
}
