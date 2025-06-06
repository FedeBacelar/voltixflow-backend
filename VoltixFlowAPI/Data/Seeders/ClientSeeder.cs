using VoltixFlowAPI.Models;

namespace VoltixFlowAPI.Data.Seeders {
	public static class ClientSeeder {
		public static void Seed(VoltixDbContext context) {
			if (!context.Clients.Any()) {
				var particular = context.ClientTypes.First(t => t.Name == "Particular");
				var mayorista = context.ClientTypes.First(t => t.Name == "Mayorista");

				var clients = new[]
				{
					new Client {
						Name         = "Juan Pérez",
						ClientTypeId = particular.Id,
						Cuit         = "20-12345678-9",
						Phone        = "11-1234-5678",
						Contact      = "María Gómez",
						Email        = "juan.perez@mail.com",
						Address      = "Calle Falsa 123",
						Status       = true,
						Observations = "Cliente particular de prueba"
					},
					new Client {
						Name         = "ACME S.A.",
						ClientTypeId = mayorista.Id,
						Cuit         = "30-87654321-0",
						Phone        = "11-8765-4321",
						Contact      = "Ing. López",
						Email        = "ventas@acme.com",
						Address      = "Av. Siempre Viva 742",
						Status       = true,
						Observations = "Mayorista de electrónica"
					}
				};

				context.Clients.AddRange(clients);
				context.SaveChanges();
			}
		}
	}
}
