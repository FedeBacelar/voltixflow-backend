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
						Username     = "Rodrigo Acosta",
						RoleId       = adminRole.Id,
						Active       = true,
						PasswordHash = hasher.HashPassword(null, "clave123")
					},
					new User {
						Username     = "Mariana Herrera",
						RoleId       = supRole.Id,
						Active       = true,
						PasswordHash = hasher.HashPassword(null, "clave123")
					},
					new User {
						Username     = "Pablo Medina",
						RoleId       = depoRole.Id,
						Active       = true,
						PasswordHash = hasher.HashPassword(null, "clave123")
					},
					new User {
						Username     = "Juan Castro",
						RoleId       = vendRole.Id,
						Active       = true,
						PasswordHash = hasher.HashPassword(null, "clave123")
					},
					new User {
						Username     = "Sofia Hernandez",
						RoleId       = vendRole.Id,
						Active       = true,
						PasswordHash = hasher.HashPassword(null, "clave123")
					},
					new User {
						Username     = "Carlos Ramos",
						RoleId       = asisRole.Id,
						Active       = true,
						PasswordHash = hasher.HashPassword(null, "clave123")
					}
				};

				context.Users.AddRange(users);
				context.SaveChanges();
			}
		}
	}
}
