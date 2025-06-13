using VoltixFlowAPI.Models;

namespace VoltixFlowAPI.Data.Seeders {
	public static class DeliveryNoteSeeder {
		public static void Seed(VoltixDbContext context) {
			if (!context.DeliveryNotes.Any()) {
				var client = context.Clients.First(c => c.Name == "ACME S.A.");
				var status = context.DeliveryNoteStatuses.First(s => s.Status == "Pendiente");
				var tvProd = context.Products.First(p => p.InternalCode == "TV-001");

				var note = new DeliveryNote {
					ClientId = client.Id,
					StatusId = status.Id,
					CreatedAt = DateTime.UtcNow,
					Observations = "Pedido de prueba",
					CreatedByUserId = context.Users.First(u => u.Username == "Rodrigo Acosta").Id
				};
				context.DeliveryNotes.Add(note);
				context.SaveChanges();

				context.DeliveryNoteItems.Add(new DeliveryNoteItem {
					DeliveryNoteId = note.Id,
					ProductId = tvProd.Id,
					Quantity = 10,
					CreatedAt = DateTime.UtcNow
				});
				context.SaveChanges();
			}
		}
	}
}
