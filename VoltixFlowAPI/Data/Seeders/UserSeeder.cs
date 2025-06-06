using Microsoft.AspNetCore.Identity;
using VoltixFlowAPI.Models;

namespace VoltixFlowAPI.Data.Seeders {
	public static class UserSeeder {
		public static void Seed(VoltixDbContext context, IServiceProvider services) {
			if (!context.Users.Any()) {
				var hasher = services.GetRequiredService<IPasswordHasher<User>>();

				var adminRole = context.Roles.First(r => r.Name == "Administrador");
				var supRole = context.Roles.First(r => r.Name == "Supervisor");
				var depoRole = context.Roles.First(r => r.Name == "Deposito");
				var vendRole = context.Roles.First(r => r.Name == "Vendedor");
				var asisRole = context.Roles.First(r => r.Name == "Asistente");

				var users = new List<User>
				{
					new User {
						Username     = "admin",
						RoleId       = adminRole.Id,
						Active       = true,
						PasswordHash = hasher.HashPassword(null, "admin123")
					},
					new User {
						Username     = "supervisor1",
						RoleId       = supRole.Id,
						Active       = true,
						PasswordHash = hasher.HashPassword(null, "super123")
					},
					new User {
						Username     = "depo1",
						RoleId       = depoRole.Id,
						Active       = true,
						PasswordHash = hasher.HashPassword(null, "depo123")
					},
					new User {
						Username     = "vendedor1",
						RoleId       = vendRole.Id,
						Active       = true,
						PasswordHash = hasher.HashPassword(null, "vent123")
					},
					new User {
						Username     = "asistente1",
						RoleId       = asisRole.Id,
						Active       = true,
						PasswordHash = hasher.HashPassword(null, "asis123")
					}
				};

				context.Users.AddRange(users);
				context.SaveChanges();
			}
		}
	}
}
