using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VoltixFlowAPI.Dtos;
using VoltixFlowAPI.Models;
using VoltixFlowAPI.Services.Interfaces;

namespace VoltixFlowAPI.Controllers {
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : ControllerBase {
		private readonly IUserService _userService;

		public UserController(IUserService userService) => _userService = userService;

		private static UserDto ToDto(User user) => new() {
			Id = user.Id,
			Username = user.Username,
			Active = user.Active,
			Role = new RoleDto {
				Id = user.Role.Id,
				Name = user.Role.Name
			}
		};

		[HttpGet]
		[Authorize(Policy = "users.list")]
		public async Task<ActionResult<IEnumerable<UserDto>>> GetAll([FromQuery] string? name,[FromQuery] int? roleId,[FromQuery] int pageNumber = 1,[FromQuery] int pageSize = 10) {
			var users = await _userService.GetUsersAsync(name, roleId, pageNumber, pageSize);
			return Ok(users.Select(ToDto));
		}

		[HttpGet("{id}")]
		[Authorize(Policy = "users.view")]
		public async Task<ActionResult<UserDto>> GetById(int id) {
			var user = await _userService.GetUserByIdAsync(id);
			if (user == null) return NotFound();
			return Ok(ToDto(user));
		}

		[HttpPost]
		[Authorize(Policy = "users.create")]
		public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserDto dto) {
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var user = await _userService.CreateUserAsync(dto);
			return CreatedAtAction(nameof(GetById), new { id = user.Id }, ToDto(user));
		}

		[HttpPut("{id}")]
		[Authorize(Policy = "users.update")]
		public async Task<ActionResult<UserDto>> Update(int id, [FromBody] UpdateUserDto dto) {
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var user = await _userService.UpdateUserAsync(id, dto)
					 ?? throw new KeyNotFoundException("Usuario no encontrado");

			return Ok(ToDto(user));
		}

		[HttpDelete("{id}")]
		[Authorize(Policy = "users.delete")]
		public async Task<IActionResult> Delete(int id){
			var deleted = await _userService.DeleteUserAsync(id);
			if (!deleted)
				return NotFound(new { message = "Usuario no encontrado" });
			return NoContent();
		}
	}
}
