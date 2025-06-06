using VoltixFlowAPI.Dtos;
using VoltixFlowAPI.Models;
using VoltixFlowAPI.Repositories.Interfaces;
using VoltixFlowAPI.Services.Interfaces;

namespace VoltixFlowAPI.Services {
	public class ClientService : IClientService {
		private readonly IClientRepository _clientRepo;
		private readonly IClientTypeRepository _typeRepo;

		public ClientService(IClientRepository clientRepo, IClientTypeRepository typeRepo) {
			_clientRepo = clientRepo;
			_typeRepo = typeRepo;
		}

		public async Task<Client> CreateClientAsync(CreateClientDto dto) {
			if (await _typeRepo.GetByIdAsync(dto.ClientTypeId) is null)
				throw new ArgumentException("ClientTypeId inválido");

			var client = new Client {
				Name = dto.Name,
				ClientTypeId = dto.ClientTypeId,
				Cuit = dto.Cuit,
				Phone = dto.Phone,
				Contact = dto.Contact,
				Email = dto.Email,
				Address = dto.Address,
				Status = dto.Status,
				Observations = dto.Observations ?? string.Empty
			};

			return await _clientRepo.CreateAsync(client);
		}

		public async Task<Client?> GetClientByIdAsync(int id) 
			=> await _clientRepo.GetByIdAsync(id);

		public async Task<IEnumerable<Client>> GetClientsAsync(string? name, string? cuit, string? email, int pageNumber, int pageSize)
			=> await _clientRepo.GetAllAsync(name, cuit, email, pageNumber, pageSize);

		public async Task<Client> UpdateClientAsync(int id, UpdateClientDto dto) {
			var existing = await _clientRepo.GetByIdAsync(id)
						   ?? throw new KeyNotFoundException("Client not found");

			if (await _typeRepo.GetByIdAsync(dto.ClientTypeId) is null)
				throw new ArgumentException("ClientTypeId inválido");

			existing.Name = dto.Name;
			existing.ClientTypeId = dto.ClientTypeId;
			existing.Cuit = dto.Cuit;
			existing.Phone = dto.Phone;
			existing.Contact = dto.Contact;
			existing.Email = dto.Email;
			existing.Address = dto.Address;
			existing.Status = dto.Status;
			existing.Observations = dto.Observations ?? string.Empty;

			return await _clientRepo.UpdateAsync(id, existing);
		}

		public async Task<bool> DeleteClientAsync(int id) 
			=> await _clientRepo.DeleteAsync(id);
	}
}
