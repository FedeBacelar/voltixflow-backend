using VoltixFlowAPI.Models;

namespace VoltixFlowAPI.Repositories.Interfaces {
	public interface IDeliveryNoteStatusRepository {
		Task<IEnumerable<DeliveryNoteStatus>> GetAllAsync();
		Task<DeliveryNoteStatus?> GetByIdAsync(int id);
		Task<DeliveryNoteStatus> FindByStatusAsync(string status);
	}
}
