using VoltixFlowAPI.Models;

namespace VoltixFlowAPI.Data.Seeders {
	public static class PermissionSeeder {
		public static void Seed(VoltixDbContext context) {
			if (context.Permissions.Any()) return;

			var permissions = new List<Permission>
			{
                new Permission { Name = "clients.navigate" },
				new Permission { Name = "clients.list" },
				new Permission { Name = "clients.view" },
				new Permission { Name = "clients.type.list" },
				new Permission { Name = "clients.create" },
				new Permission { Name = "clients.update" },
				new Permission { Name = "clients.delete" },

                new Permission { Name = "products.navigate" },
				new Permission { Name = "products.list" },
				new Permission { Name = "products.view" },
				new Permission { Name = "products.create" },
				new Permission { Name = "products.update" },
				new Permission { Name = "products.delete" },
				new Permission { Name = "products.stock.reserved" },
				new Permission { Name = "products.stock.real" },
				new Permission { Name = "products.categories.list" },
				new Permission { Name = "products.categories.create" },
				new Permission { Name = "products.categories.delete" },

                new Permission { Name = "delivery-notes.navigate" },
				new Permission { Name = "delivery-notes.list" },
				new Permission { Name = "delivery-notes.view" },
				new Permission { Name = "delivery-notes.create" },
				new Permission { Name = "delivery-notes.update" },
				new Permission { Name = "delivery-notes.delete" },
				new Permission { Name = "delivery-notes.deliver" },
				new Permission { Name = "delivery-notes.cancel" },

                new Permission { Name = "users.navigate" },
				new Permission { Name = "users.list" },
				new Permission { Name = "users.view" },
				new Permission { Name = "users.create" },
				new Permission { Name = "users.update" },
				new Permission { Name = "users.delete" }
			};

			context.Permissions.AddRange(permissions);
			context.SaveChanges();
		}
	}
}
