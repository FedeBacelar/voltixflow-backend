using VoltixFlowAPI.Data;
using VoltixFlowAPI.Dtos;
using VoltixFlowAPI.Models;
using VoltixFlowAPI.Repositories.Interfaces;
using VoltixFlowAPI.Services.Interfaces;

namespace VoltixFlowAPI.Services {
	public class DeliveryNoteService : IDeliveryNoteService {
		private readonly IProductRepository _productRepo;
		private readonly IClientRepository _clientRepo;
		private readonly IDeliveryNoteRepository _noteRepo;
		private readonly IDeliveryNoteItemRepository _itemRepo;
		private readonly IDeliveryNoteStatusRepository _statusRepo;
		private readonly VoltixDbContext _context;

		public DeliveryNoteService(IProductRepository productRepo, IClientRepository clientRepo, IDeliveryNoteRepository noteRepo, IDeliveryNoteItemRepository itemRepo, IDeliveryNoteStatusRepository statusRepo, VoltixDbContext context) {
			_productRepo = productRepo;
			_clientRepo = clientRepo;
			_noteRepo = noteRepo;
			_itemRepo = itemRepo;
			_statusRepo = statusRepo;
			_context = context;
		}

		public async Task<DeliveryNote> CreateAsync(CreateDeliveryNoteDto dto) {
			if (await _clientRepo.GetByIdAsync(dto.ClientId) is null)
				throw new ArgumentException("ClientId inválido");

			var pend = await _statusRepo.GetByIdAsync(dto.StatusId)
					   ?? throw new ArgumentException("StatusId inválido");
			if (pend.Status != "Pendiente")
				throw new InvalidOperationException("Sólo se puede crear con status Pendiente");

			foreach (var itemDto in dto.Items) {
				var prod = await _productRepo.GetByIdAsync(itemDto.ProductId)
						   ?? throw new ArgumentException($"ProductId {itemDto.ProductId} no existe");
				if (prod.CurrentStock < itemDto.Quantity)
					throw new InvalidOperationException($"Stock insuficiente para {prod.Name}");
				prod.CurrentStock -= itemDto.Quantity;
			}
			await _context.SaveChangesAsync();

			var note = new DeliveryNote {
				ClientId = dto.ClientId,
				StatusId = dto.StatusId,
				CreatedAt = DateTime.UtcNow,
				Observations = dto.Observations ?? string.Empty,
				CreatedByUserId = 0
			};
			await _noteRepo.CreateAsync(note);

			var items = dto.Items.Select(i => new DeliveryNoteItem {
				DeliveryNoteId = note.Id,
				ProductId = i.ProductId,
				Quantity = i.Quantity,
				CreatedAt = DateTime.UtcNow
			}).ToList();
			await _itemRepo.AddRangeAsync(items);

			return note;
		}

		public Task<IEnumerable<DeliveryNote>> GetAllAsync(
			string? clientName, int? statusId, int pageNumber, int pageSize)
			=> _noteRepo.GetAllAsync(clientName, statusId, pageNumber, pageSize);

		public Task<DeliveryNote?> GetByIdAsync(int id)
			=> _noteRepo.GetByIdAsync(id);

		public async Task<DeliveryNote> UpdateAsync(int id, UpdateDeliveryNoteDto dto) {
			var note = await _noteRepo.GetByIdAsync(id)
					   ?? throw new KeyNotFoundException("Remito no encontrado");
			if (note.Status.Status != "Pendiente")
				throw new InvalidOperationException("Sólo se puede modificar Pendiente");

			var oldItems = await _itemRepo.GetByNoteIdAsync(id);
			foreach (var old in oldItems) {
				var prod = await _productRepo.GetByIdAsync(old.ProductId);
				prod.CurrentStock += old.Quantity;
			}
			await _itemRepo.DeleteByNoteIdAsync(id);
			await _context.SaveChangesAsync();

			foreach (var itemDto in dto.Items) {
				var prod = await _productRepo.GetByIdAsync(itemDto.ProductId)
						   ?? throw new ArgumentException($"ProductId {itemDto.ProductId} no existe");
				if (prod.CurrentStock < itemDto.Quantity)
					throw new InvalidOperationException($"Stock insuficiente para {prod.Name}");
				prod.CurrentStock -= itemDto.Quantity;
			}
			await _context.SaveChangesAsync();

			note.StatusId = dto.StatusId;
			note.Observations = dto.Observations ?? string.Empty;
			await _noteRepo.UpdateAsync(id, note);

			var items = dto.Items.Select(i => new DeliveryNoteItem {
				DeliveryNoteId = id,
				ProductId = i.ProductId,
				Quantity = i.Quantity,
				CreatedAt = DateTime.UtcNow
			}).ToList();
			await _itemRepo.AddRangeAsync(items);

			return note;
		}

		public async Task<bool> CancelAsync(int id) {
			var note = await _noteRepo.GetByIdAsync(id)
					   ?? throw new KeyNotFoundException("Remito no encontrado");
			if (note.Status.Status != "Pendiente")
				throw new InvalidOperationException("Sólo se puede anular Pendiente");

			var items = await _itemRepo.GetByNoteIdAsync(id);
			foreach (var it in items) {
				var prod = await _productRepo.GetByIdAsync(it.ProductId);
				prod.CurrentStock += it.Quantity;
			}
			note.StatusId = (await _statusRepo.FindByStatusAsync("Anulado")).Id;
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeliverAsync(int id) {
			var note = await _noteRepo.GetByIdAsync(id)
					   ?? throw new KeyNotFoundException("Remito no encontrado");
			if (note.Status.Status != "Pendiente")
				throw new InvalidOperationException("Sólo se puede entregar Pendiente");

			note.StatusId = (await _statusRepo.FindByStatusAsync("Entregado")).Id;
			note.DeliveredAt = DateTime.UtcNow;
			await _noteRepo.UpdateAsync(id, note);
			return true;
		}

		public async Task<bool> DeleteAsync(int id) {
			var note = await _noteRepo.GetByIdAsync(id)
					   ?? throw new KeyNotFoundException("Remito no encontrado");

			if (note.Status.Status == "Entregado")
				throw new InvalidOperationException("No se puede borrar un Entregado sin reversión");

			if (note.Status.Status == "Pendiente") {
				var items = await _itemRepo.GetByNoteIdAsync(id);
				foreach (var it in items) {
					var prod = await _productRepo.GetByIdAsync(it.ProductId);
					prod.CurrentStock += it.Quantity;
				}
			}
			await _itemRepo.DeleteByNoteIdAsync(id);
			return await _noteRepo.DeleteAsync(id);
		}
	}
}
