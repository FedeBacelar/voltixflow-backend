using VoltixFlowAPI.Models;

namespace VoltixFlowAPI.Repositories.Interfaces {
	public interface IClientTypeRepository {
		Task<IEnumerable<ClientType>> GetAllAsync();
		Task<ClientType?> GetByIdAsync(int id);
	}
}
