using Microsoft.EntityFrameworkCore;
using VoltixFlowAPI.Data;
using VoltixFlowAPI.Models;
using VoltixFlowAPI.Repositories.Interfaces;

namespace VoltixFlowAPI.Repositories {
	public class ClientTypeRepository : IClientTypeRepository {
		private readonly VoltixDbContext _context;
		public ClientTypeRepository(VoltixDbContext context) => _context = context;

		public async Task<IEnumerable<ClientType>> GetAllAsync() {
			return await _context.ClientTypes.ToListAsync();
		}

		public async Task<ClientType?> GetByIdAsync(int id) {
			return await _context.ClientTypes.FindAsync(id);
		}
	}
}
