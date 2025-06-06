using Microsoft.AspNetCore.Identity;
using VoltixFlowAPI.Dtos;
using VoltixFlowAPI.Models;
using VoltixFlowAPI.Repositories.Interfaces;
using VoltixFlowAPI.Services.Interfaces;

namespace VoltixFlowAPI.Services {
	public class UserService : IUserService {
		private readonly IUserRepository _userRepo;
		private readonly IRoleRepository _roleRepo;
		private readonly IPasswordHasher<User> _hasher;

		public UserService(IUserRepository userRepo, IRoleRepository roleRepo, IPasswordHasher<User> hasher) {
			_userRepo = userRepo;
			_roleRepo = roleRepo;
			_hasher = hasher;
		}

		public async Task<User> CreateUserAsync(CreateUserDto dto) {
			if (await _roleRepo.GetByIdAsync(dto.RoleId) is null)
				throw new ArgumentException("RoleId inválido");

			if (await _userRepo.FindByUsernameAsync(dto.Username) != null)
				throw new ArgumentException("El nombre de usuario ya existe");

			var user = new User {
				Username = dto.Username,
				RoleId = dto.RoleId,
				Active = true
			};
			user.PasswordHash = _hasher.HashPassword(user, dto.Password);

			return await _userRepo.CreateAsync(user);
		}

		public async Task<User?> GetUserByIdAsync(int id) =>
			await _userRepo.GetByIdAsync(id);

		public async Task<User?> GetByUsernameAsync(string username) =>
			await _userRepo.FindByUsernameAsync(username);

		public async Task<IEnumerable<User>> GetUsersAsync(string? name, int? roleId, int pageNumber, int pageSize) =>
			await _userRepo.GetAllAsync(name, roleId, pageNumber, pageSize);

		public async Task<User> UpdateUserAsync(int id, UpdateUserDto dto) {
			var existing = await _userRepo.GetByIdAsync(id)
						 ?? throw new KeyNotFoundException("Usuario no encontrado");

			if (await _roleRepo.GetByIdAsync(dto.RoleId) is null)
				throw new ArgumentException("RoleId inválido");

			if (existing.Username != dto.Username &&
				await _userRepo.FindByUsernameAsync(dto.Username) != null)
				throw new ArgumentException("El nombre de usuario ya existe");

			existing.Username = dto.Username;
			existing.RoleId = dto.RoleId;
			existing.Active = dto.Active;

			if (!string.IsNullOrWhiteSpace(dto.Password))
				existing.PasswordHash = _hasher.HashPassword(existing, dto.Password);

			return await _userRepo.UpdateAsync(id, existing);
		}

		public async Task<bool> DeleteUserAsync(int id) => await _userRepo.DeleteAsync(id);
	}
}
