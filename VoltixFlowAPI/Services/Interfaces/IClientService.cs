using VoltixFlowAPI.Dtos;
using VoltixFlowAPI.Models;

namespace VoltixFlowAPI.Services.Interfaces {
	public interface IClientService {
		Task<Client> CreateClientAsync(CreateClientDto dto);
		Task<Client?> GetClientByIdAsync(int id);
		Task<IEnumerable<Client>> GetClientsAsync(string? name, string? cuit, string? email, int pageNumber, int pageSize);
		Task<Client> UpdateClientAsync(int id, UpdateClientDto dto);
		Task<bool> DeleteClientAsync(int id);
	}
}
