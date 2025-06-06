using Microsoft.EntityFrameworkCore;
using VoltixFlowAPI.Data;
using VoltixFlowAPI.Models;
using VoltixFlowAPI.Repositories.Interfaces;

namespace VoltixFlowAPI.Repositories {
	public class UserRepository : IUserRepository {
		private readonly VoltixDbContext _context;

		public UserRepository(VoltixDbContext context) => _context = context;

		public async Task<User> CreateAsync(User user) {
			_context.Users.Add(user);
			await _context.SaveChangesAsync();
			return user;
		}

		public async Task<User?> GetByIdAsync(int id) {
			return await _context.Users
				.Include(user => user.Role)
				.FirstOrDefaultAsync(user => user.Id == id);
		}

		public async Task<User?> FindByUsernameAsync(string username) {
			return await _context.Users
				.Include(user => user.Role)
				.FirstOrDefaultAsync(user => user.Username == username);
		}

		public async Task<IEnumerable<User>> GetAllAsync(string? name, int? roleId, int pageNumber, int pageSize) {
			var query = _context.Users.Include(user => user.Role).AsQueryable();

			if (!string.IsNullOrWhiteSpace(name))
				query = query.Where(user => user.Username.Contains(name));

			if (roleId.HasValue)
				query = query.Where(user => user.RoleId == roleId.Value);

			return await query
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();
		}

		public async Task<User> UpdateAsync(int id, User updatedUser) {
			var user = await _context.Users.FindAsync(id);
			if (user == null) throw new KeyNotFoundException("User not found");

			user.Username = updatedUser.Username;
			user.PasswordHash = updatedUser.PasswordHash;
			user.RoleId = updatedUser.RoleId;
			user.Active = updatedUser.Active;

			await _context.SaveChangesAsync();
			return user;
		}

		public async Task<bool> DeleteAsync(int id){
			var user = await _context.Users.FindAsync(id);
			if (user == null) return false;
			_context.Users.Remove(user);
			await _context.SaveChangesAsync();
			return true;
		}
}
}
