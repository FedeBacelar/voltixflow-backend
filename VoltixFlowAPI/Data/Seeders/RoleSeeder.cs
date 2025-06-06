using VoltixFlowAPI.Models;

namespace VoltixFlowAPI.Data.Seeders {
	public static class RoleSeeder {
		public static void Seed(VoltixDbContext context) {
			if (!context.Roles.Any()) {
				var roles = new List<Role>
				{
					new Role { Name = "Administrador" },
					new Role { Name = "Supervisor" },
					new Role { Name = "Deposito" },
					new Role { Name = "Vendedor" },
					new Role { Name = "Asistente" }
				};

				context.Roles.AddRange(roles);
				context.SaveChanges();
			}
		}
	}
}
