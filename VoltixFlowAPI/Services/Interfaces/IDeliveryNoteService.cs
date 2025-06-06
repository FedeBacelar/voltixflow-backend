using VoltixFlowAPI.Dtos;
using VoltixFlowAPI.Models;

namespace VoltixFlowAPI.Services.Interfaces {
	public interface IDeliveryNoteService {
		Task<DeliveryNote> CreateAsync(CreateDeliveryNoteDto dto);
		Task<IEnumerable<DeliveryNote>> GetAllAsync(
			string? clientName, int? statusId, int pageNumber, int pageSize);
		Task<DeliveryNote?> GetByIdAsync(int id);
		Task<DeliveryNote> UpdateAsync(int id, UpdateDeliveryNoteDto dto);
		Task<bool> DeleteAsync(int id);
		Task<bool> CancelAsync(int id);
		Task<bool> DeliverAsync(int id);
	}
}
