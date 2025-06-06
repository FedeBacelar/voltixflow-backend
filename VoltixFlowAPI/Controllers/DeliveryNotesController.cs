using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using VoltixFlowAPI.Dtos;
using VoltixFlowAPI.Models;
using VoltixFlowAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace VoltixFlowAPI.Controllers {
	[ApiController]
	[Route("api/[controller]")]
	public class DeliveryNotesController : ControllerBase {

		private readonly IDeliveryNoteService _deliveryNoteService;

		public DeliveryNotesController(IDeliveryNoteService deliveryNoteService) => _deliveryNoteService = deliveryNoteService;

		private DeliveryNoteReadDto ToDto(DeliveryNote deliveryNote) => new() {
			Id = deliveryNote.Id,
			Client = new ClientMiniDto {
				Id = deliveryNote.Client.Id,
				Name = deliveryNote.Client.Name
			},
			Status = new DeliveryNoteStatusDto {
				Id = deliveryNote.Status.Id,
				Status = deliveryNote.Status.Status
			},
			CreatedAt = deliveryNote.CreatedAt,
			DeliveredAt = deliveryNote.DeliveredAt,
			Observations = deliveryNote.Observations,
			Items = deliveryNote.Items.Select(item => new DeliveryNoteItemReadDto {
				ProductId = item.ProductId,
				ProductCode = item.Product.InternalCode,
				ProductName = item.Product.Name,
				Quantity = item.Quantity
			}).ToList()
		};

		[HttpGet("{id}")]
		[Authorize(Policy = "delivery-notes.view")]
		public async Task<ActionResult<DeliveryNoteReadDto>> GetById(int id) {
			var deliveryNote = await _deliveryNoteService.GetByIdAsync(id);
			if (deliveryNote == null) return NotFound();
			return Ok(ToDto(deliveryNote));
		}

		[HttpGet]
		[Authorize(Policy = "delivery-notes.list")]
		public async Task<ActionResult<IEnumerable<DeliveryNoteReadDto>>> GetAll([FromQuery] string? clientName, [FromQuery] int? statusId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10) {
			var list = await _deliveryNoteService.GetAllAsync(clientName, statusId, pageNumber, pageSize);
			return Ok(list.Select(ToDto));
		}

		[HttpPost]
		[Authorize(Policy = "delivery-notes.create")]
		public async Task<ActionResult<DeliveryNoteReadDto>> Create([FromBody] CreateDeliveryNoteDto dto) {
			if (!ModelState.IsValid) return BadRequest(ModelState);
			var deliveryNote = await _deliveryNoteService.CreateAsync(dto);
			return CreatedAtAction(nameof(GetById), new { id = deliveryNote.Id }, ToDto(deliveryNote));
		}

		[HttpPut("{id}")]
		[Authorize(Policy = "delivery-notes.update")]
		public async Task<ActionResult<DeliveryNoteReadDto>> Update(int id, [FromBody] UpdateDeliveryNoteDto dto) {
			if (!ModelState.IsValid) return BadRequest(ModelState);
			var deliveryNote = await _deliveryNoteService.UpdateAsync(id, dto);
			return Ok(ToDto(deliveryNote));
		}

		[HttpDelete("{id}")]
		[Authorize(Policy = "delivery-notes.delete")]
		public async Task<IActionResult> Delete(int id) {
			var ok = await _deliveryNoteService.DeleteAsync(id);
			if (!ok) return NotFound();
			return NoContent();
		}

		[HttpPatch("{id}/cancel")]
		[Authorize(Policy = "delivery-notes.cancel")]
		public async Task<IActionResult> Cancel(int id) {
			var ok = await _deliveryNoteService.CancelAsync(id);
			if (!ok) return BadRequest();
			return NoContent();
		}

		[HttpPatch("{id}/deliver")]
		[Authorize(Policy = "delivery-notes.deliver")]
		public async Task<IActionResult> Deliver(int id) {
			var ok = await _deliveryNoteService.DeliverAsync(id);
			if (!ok) return BadRequest();
			return NoContent();
		}
	}
}
