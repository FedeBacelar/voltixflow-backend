using VoltixFlowAPI.Models;

namespace VoltixFlowAPI.Data.Seeders {
	public static class DeliveryNoteStatusSeeder {
		public static void Seed(VoltixDbContext context) {
			if (!context.DeliveryNoteStatuses.Any()) {
				context.DeliveryNoteStatuses.AddRange(new[]
				{
					new DeliveryNoteStatus { Status = "Pendiente" },
					new DeliveryNoteStatus { Status = "Entregado" }
				});
				context.SaveChanges();
			}
		}
	}
}
