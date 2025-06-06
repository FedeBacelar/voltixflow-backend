using Microsoft.EntityFrameworkCore;
using VoltixFlowAPI.Data;
using VoltixFlowAPI.Models;
using VoltixFlowAPI.Repositories.Interfaces;

namespace VoltixFlowAPI.Repositories {
	public class DeliveryNoteStatusRepository : IDeliveryNoteStatusRepository {
		private readonly VoltixDbContext _context;
		public DeliveryNoteStatusRepository(VoltixDbContext context) => _context = context;

		public async Task<IEnumerable<DeliveryNoteStatus>> GetAllAsync() =>
			await _context.DeliveryNoteStatuses.ToListAsync();

		public Task<DeliveryNoteStatus?> GetByIdAsync(int id) =>
			_context.DeliveryNoteStatuses.FirstOrDefaultAsync(deliveryNoteStatus => deliveryNoteStatus.Id == id);

		public Task<DeliveryNoteStatus> FindByStatusAsync(string status) =>
			_context.DeliveryNoteStatuses.FirstAsync(deliveryNoteStatus => deliveryNoteStatus.Status == status);
	}
}
