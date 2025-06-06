using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VoltixFlowAPI.Dtos;
using VoltixFlowAPI.Models;
using VoltixFlowAPI.Services.Interfaces;

namespace VoltixFlowAPI.Controllers {
	[ApiController]
	[Route("api/[controller]")]
	public class ClientController : ControllerBase {
		private readonly IClientService _clientService;

		public ClientController(IClientService clientService) => _clientService = clientService;

		private static ClientDto ToDto(Client client) => new() {
			Id = client.Id,
			Name = client.Name,
			ClientType = new ClientTypeDto { Id = client.ClientType.Id, Name = client.ClientType.Name },
			Cuit = client.Cuit,
			Phone = client.Phone,
			Contact = client.Contact,
			Email = client.Email,
			Address = client.Address,
			Status = client.Status,
			Observations = client.Observations
		};

		[HttpGet]
		[Authorize(Policy = "clients.list")]
		public async Task<ActionResult<IEnumerable<ClientDto>>> GetAll([FromQuery] string? name, [FromQuery] string? cuit, [FromQuery] string? email, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10) {
			var clients = await _clientService.GetClientsAsync(name, cuit, email, pageNumber, pageSize);
			return Ok(clients.Select(ToDto));
		}

		[HttpGet("{id}")]
		[Authorize(Policy = "clients.view")]
		public async Task<ActionResult<ClientDto>> GetById(int id) {
			var client = await _clientService.GetClientByIdAsync(id);
			if (client == null) return NotFound();
			return Ok(ToDto(client));
		}

		[HttpPost]
		[Authorize(Policy = "clients.create")]
		public async Task<ActionResult<ClientDto>> Create([FromBody] CreateClientDto dto) {
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var client = await _clientService.CreateClientAsync(dto);
			return CreatedAtAction(nameof(GetById), new { id = client.Id }, ToDto(client));
		}

		[HttpPut("{id}")]
		[Authorize(Policy = "clients.update")]
		public async Task<ActionResult<ClientDto>> Update(int id, [FromBody] UpdateClientDto dto) {
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var client = await _clientService.UpdateClientAsync(id, dto)
					?? throw new KeyNotFoundException("Client not found");

			return Ok(ToDto(client));
		}

		[HttpDelete("{id}")]
		[Authorize(Policy = "clients.delete")]
		public async Task<IActionResult> Delete(int id) {
			var deleted = await _clientService.DeleteClientAsync(id);
			if (!deleted)
				return NotFound(new { message = "Client not found" });

			return NoContent();
		}
	}
}
