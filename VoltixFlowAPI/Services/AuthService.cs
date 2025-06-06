using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using VoltixFlowAPI.Dtos;
using VoltixFlowAPI.Models;
using VoltixFlowAPI.Repositories;
using VoltixFlowAPI.Repositories.Interfaces;
using VoltixFlowAPI.Services.Interfaces;

namespace VoltixFlowAPI.Services {
	public class AuthService : IAuthService {
		private readonly IUserRepository _userRepo;
		private readonly IRoleRepository _roleRepo;
		private readonly IRolePermissionRepository _rpRepo;
		private readonly IPasswordHasher<User> _hasher;
		private readonly IConfiguration _cfg;

		public AuthService(IUserRepository userRepo, IRoleRepository roleRepo, IRolePermissionRepository rpRepo, IPasswordHasher<User> hasher, IConfiguration cfg) {
			_userRepo = userRepo;
			_roleRepo = roleRepo;
			_rpRepo = rpRepo;
			_hasher = hasher;
			_cfg = cfg;
		}

		public async Task<LoginResponseDto> LoginAsync(string username, string password) {
			var user = await _userRepo.FindByUsernameAsync(username)
					   ?? throw new ArgumentException("Usuario no encontrado");
			var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);
			if (result == PasswordVerificationResult.Failed)
				throw new ArgumentException("Credenciales inválidas");

			var role = await _roleRepo.GetByIdAsync(user.RoleId)
					   ?? throw new ArgumentException("Rol no encontrado");
			var perms = await _rpRepo.GetByRoleIdAsync(user.RoleId);
			var permNames = perms.Select(rp => rp.Permission.Name);

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_cfg["Jwt:Key"]!));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expires = DateTime.UtcNow.AddMinutes(int.Parse(_cfg["Jwt:ExpiresMinutes"]!));

			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
				new Claim(ClaimTypes.Role, role.Name)
			};
			claims.AddRange(permNames.Select(p => new Claim("permission", p)));

			var token = new JwtSecurityToken(
				issuer: _cfg["Jwt:Issuer"],
				audience: _cfg["Jwt:Audience"],
				claims: claims,
				expires: expires,
				signingCredentials: creds
			);

			return new LoginResponseDto {
				Token = new JwtSecurityTokenHandler().WriteToken(token),
				ExpiresAt = expires,
				UserId = user.Id,
				Username = user.Username,
				Role = role.Name,
				Permissions = permNames
			};
		}
	}
}
