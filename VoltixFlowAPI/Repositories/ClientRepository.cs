using Microsoft.EntityFrameworkCore;
using VoltixFlowAPI.Data;
using VoltixFlowAPI.Models;
using VoltixFlowAPI.Repositories.Interfaces;

namespace VoltixFlowAPI.Repositories {
	public class ClientRepository : IClientRepository {
		private readonly VoltixDbContext _context;
		public ClientRepository(VoltixDbContext context) => _context = context;

		public async Task<Client> CreateAsync(Client client) {
			_context.Clients.Add(client);
			await _context.SaveChangesAsync();
			return client;
		}

		public async Task<Client?> GetByIdAsync(int id) {
			return await _context.Clients
				.Include(client => client.ClientType)
				.FirstOrDefaultAsync(client => client.Id == id);
		}

		public async Task<IEnumerable<Client>> GetAllAsync(string? name, string? cuit, string? email, int pageNumber, int pageSize) {
			var query = _context.Clients
				.Include(client => client.ClientType)
				.AsQueryable();

			if (!string.IsNullOrWhiteSpace(name))
				query = query.Where(client => client.Name.Contains(name));

			if (!string.IsNullOrWhiteSpace(cuit))
				query = query.Where(client => client.Cuit.Contains(cuit));

			if (!string.IsNullOrWhiteSpace(email))
				query = query.Where(client => client.Email.Contains(email));

			return await query
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();
		}

		public async Task<Client> UpdateAsync(int id, Client updatedClient) {
			var client = await _context.Clients.FindAsync(id);
			if (client == null) throw new KeyNotFoundException("Client not found");

			client.Name = updatedClient.Name;
			client.ClientTypeId = updatedClient.ClientTypeId;
			client.Cuit = updatedClient.Cuit;
			client.Phone = updatedClient.Phone;
			client.Contact = updatedClient.Contact;
			client.Email = updatedClient.Email;
			client.Address = updatedClient.Address;
			client.Status = updatedClient.Status;
			client.Observations = updatedClient.Observations;

			await _context.SaveChangesAsync();
			return client;
		}

		public async Task<bool> DeleteAsync(int id) {
			var client = await _context.Clients.FindAsync(id);
			if (client == null) return false;

			_context.Clients.Remove(client);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
