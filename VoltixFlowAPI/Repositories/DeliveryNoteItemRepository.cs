using Microsoft.EntityFrameworkCore;
using VoltixFlowAPI.Data;
using VoltixFlowAPI.Models;
using VoltixFlowAPI.Repositories.Interfaces;

namespace VoltixFlowAPI.Repositories {
	public class DeliveryNoteItemRepository : IDeliveryNoteItemRepository {
		private readonly VoltixDbContext _context;
		public DeliveryNoteItemRepository(VoltixDbContext context) => _context = context;

		public async Task AddRangeAsync(IEnumerable<DeliveryNoteItem> items) {
			_context.DeliveryNoteItems.AddRange(items);
			await _context.SaveChangesAsync();
		}

		public Task<IEnumerable<DeliveryNoteItem>> GetByNoteIdAsync(int noteId) =>
			_context.DeliveryNoteItems
				.Where(item => item.DeliveryNoteId == noteId)
				.ToListAsync()
				.ContinueWith(t => (IEnumerable<DeliveryNoteItem>)t.Result);

		public async Task DeleteByNoteIdAsync(int noteId) {
			var items = _context.DeliveryNoteItems.Where(item => item.DeliveryNoteId == noteId);
			_context.DeliveryNoteItems.RemoveRange(items);
			await _context.SaveChangesAsync();
		}

		public async Task<int> GetReservedStockAsync(int productId) =>
			await _context.DeliveryNoteItems
				.Where(item => item.ProductId == productId && item.DeliveryNote.Status.Status == "Pendiente")
				.SumAsync(item => (int?)item.Quantity) ?? 0;
	}
}
