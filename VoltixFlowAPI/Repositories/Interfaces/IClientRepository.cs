using VoltixFlowAPI.Models;

namespace VoltixFlowAPI.Repositories.Interfaces {
	public interface IClientRepository {
		Task<Client> CreateAsync(Client client);
		Task<Client?> GetByIdAsync(int id);
		Task<IEnumerable<Client>> GetAllAsync(string? name, string? cuit, string? email, int pageNumber, int pageSize);
		Task<Client> UpdateAsync(int id, Client updatedClient);
		Task<bool> DeleteAsync(int id);
	}
}
