using VoltixFlowAPI.Models;

namespace VoltixFlowAPI.Repositories.Interfaces {
	public interface IDeliveryNoteItemRepository {
		Task AddRangeAsync(IEnumerable<DeliveryNoteItem> items);
		Task<IEnumerable<DeliveryNoteItem>> GetByNoteIdAsync(int noteId);
		Task DeleteByNoteIdAsync(int noteId);
		Task<int> GetReservedStockAsync(int productId);
	}
}
