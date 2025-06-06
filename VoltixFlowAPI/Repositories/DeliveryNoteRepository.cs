using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoltixFlowAPI.Data;
using VoltixFlowAPI.Models;
using VoltixFlowAPI.Repositories.Interfaces;

namespace VoltixFlowAPI.Repositories {
	public class DeliveryNoteRepository : IDeliveryNoteRepository {
		private readonly VoltixDbContext _context;
		public DeliveryNoteRepository(VoltixDbContext context) => _context = context;

		public async Task<DeliveryNote> CreateAsync(DeliveryNote note) {
			_context.DeliveryNotes.Add(note);
			await _context.SaveChangesAsync();
			return note;
		}

		public async Task<IEnumerable<DeliveryNote>> GetAllAsync(
			string? clientName, int? statusId, int pageNumber, int pageSize) {
			var q = _context.DeliveryNotes
						.Include(deliveryNote => deliveryNote.Client)
						.Include(deliveryNote => deliveryNote.Status)
						.AsQueryable();

			if (!string.IsNullOrWhiteSpace(clientName))
				q = q.Where(deliveryNote => deliveryNote.Client.Name.Contains(clientName));
			if (statusId.HasValue)
				q = q.Where(deliveryNote => deliveryNote.StatusId == statusId.Value);

			return await q
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();
		}

		public Task<DeliveryNote?> GetByIdAsync(int id) =>
			_context.DeliveryNotes
				.Include(deliveryNote => deliveryNote.Client)
				.Include(deliveryNote => deliveryNote.Status)
				.Include(deliveryNote => deliveryNote.Items)
					.ThenInclude(item => item.Product)
				.FirstOrDefaultAsync(deliveryNote => deliveryNote.Id == id);

		public async Task<DeliveryNote> UpdateAsync(int id, DeliveryNote updatedNote) {
			var deliveryNote = await _context.DeliveryNotes.FindAsync(id)
					  ?? throw new KeyNotFoundException("Remito no encontrado");

			_context.Entry(deliveryNote).CurrentValues.SetValues(updatedNote);
			await _context.SaveChangesAsync();
			return deliveryNote;
		}

		public async Task<bool> DeleteAsync(int id) {
			var deliveryNote = await _context.DeliveryNotes.FindAsync(id);
			if (deliveryNote == null) return false;
			_context.DeliveryNotes.Remove(deliveryNote);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
