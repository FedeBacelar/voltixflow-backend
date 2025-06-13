using VoltixFlowAPI.Models;

namespace VoltixFlowAPI.Data.Seeders {
	public static class RolePermissionSeeder {
		public static void Seed(VoltixDbContext context) {
			if (context.RolePermissions.Any()) return;

			var admin = context.Roles.First(r => r.Name == "Administrador");
			var supervisor = context.Roles.First(r => r.Name == "Supervisor");
			var deposito = context.Roles.First(r => r.Name == "Deposito");
			var vendedor = context.Roles.First(r => r.Name == "Vendedor");
			var asistente = context.Roles.First(r => r.Name == "Asistente");

			var allPermissions = context.Permissions.ToList();

			var rpList = new List<RolePermission>();

			foreach (var perm in allPermissions) {
				rpList.Add(new RolePermission {
					RoleId = admin.Id,
					PermissionId = perm.Id
				});
			}

			var supervisorAllowed = allPermissions
				.Where(p => !p.Name.StartsWith("users.") && 
							(p.Name.EndsWith(".navigate") ||
							p.Name.EndsWith(".list") ||
							p.Name.EndsWith(".view")))
				.ToList();

			foreach (var perm in supervisorAllowed) {
				rpList.Add(new RolePermission {
					RoleId = supervisor.Id,
					PermissionId = perm.Id
				});
			}

			var depositoAllowed = allPermissions
				.Where(p => p.Name.StartsWith("products.") &&
							(p.Name.EndsWith(".navigate") ||
							 p.Name.EndsWith(".list") ||
							 p.Name.EndsWith(".view") ||
							 p.Name.Contains(".create") ||
							 p.Name.Contains(".update") ||
							 p.Name.Contains(".delete") ||
							 p.Name.Contains("stock")))
				.ToList();

			foreach (var perm in depositoAllowed) {
				rpList.Add(new RolePermission {
					RoleId = deposito.Id,
					PermissionId = perm.Id
				});
			}

			var vendedorAllowed = allPermissions
				.Where(p => (p.Name.StartsWith("clients.") || p.Name.StartsWith("delivery-notes.")) &&
							(p.Name.EndsWith(".navigate") ||
							 p.Name.EndsWith(".list") ||
							 p.Name.EndsWith(".view") ||
							 p.Name.Contains(".create") ||
							 p.Name.Contains(".update") ||
							 p.Name.Contains(".delete")))
				.ToList();

			foreach (var perm in vendedorAllowed) {
				rpList.Add(new RolePermission {
					RoleId = vendedor.Id,
					PermissionId = perm.Id
				});
			}

			var asistenteAllowed = allPermissions
				.Where(p => (p.Name.StartsWith("clients.") ||
							 p.Name.StartsWith("products.")) &&
							(p.Name.EndsWith(".navigate") ||
							 p.Name.EndsWith(".list") ||
							 p.Name.EndsWith(".view")))
				.ToList();

			foreach (var perm in asistenteAllowed) {
				rpList.Add(new RolePermission {
					RoleId = asistente.Id,
					PermissionId = perm.Id
				});
			}

			context.RolePermissions.AddRange(rpList);
			context.SaveChanges();
		}
	}
}
