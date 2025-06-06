using VoltixFlowAPI.Models;

namespace VoltixFlowAPI.Data.Seeders {
	public static class ClientTypeSeeder {
		public static void Seed(VoltixDbContext context) {
			if (!context.ClientTypes.Any()) {
				var types = new List<ClientType>
				{
					new ClientType { Name = "Particular" },
					new ClientType { Name = "Mayorista" }
				};

				context.ClientTypes.AddRange(types);
				context.SaveChanges();
			}
		}
	}
}
