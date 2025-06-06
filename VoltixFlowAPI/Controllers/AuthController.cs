using Microsoft.AspNetCore.Mvc;
using VoltixFlowAPI.Dtos;
using VoltixFlowAPI.Services.Interfaces;

namespace VoltixFlowAPI.Controllers {
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase {
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService) => _authService = authService;

		[HttpPost("login")]
		public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto dto) {
			if (!ModelState.IsValid) return BadRequest(ModelState);
			var res = await _authService.LoginAsync(dto.Username, dto.Password);
			return Ok(res);
		}
	}
}
