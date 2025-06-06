using VoltixFlowAPI.Models;

namespace VoltixFlowAPI.Repositories.Interfaces {
	public interface IDeliveryNoteRepository {
		Task<DeliveryNote> CreateAsync(DeliveryNote note);
		Task<IEnumerable<DeliveryNote>> GetAllAsync(
			string? clientName, int? statusId, int pageNumber, int pageSize);
		Task<DeliveryNote?> GetByIdAsync(int id);
		Task<DeliveryNote> UpdateAsync(int id, DeliveryNote updatedNote);
		Task<bool> DeleteAsync(int id);
	}
}
