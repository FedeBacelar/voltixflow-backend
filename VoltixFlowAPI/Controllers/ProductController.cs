using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using VoltixFlowAPI.Dtos;
using VoltixFlowAPI.Models;
using VoltixFlowAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace VoltixFlowAPI.Controllers {
	[ApiController]
	[Route("api/[controller]")]
	public class ProductController : ControllerBase {
		private readonly IProductService _productService;
		public ProductController(IProductService productService) => _productService = productService;

		private static ProductDto ToDto(Product product) => new() {
			Id = product.Id,
			InternalCode = product.InternalCode,
			Name = product.Name,
			Description = product.Description,
			Category = new ProductCategoryDto {
				Id = product.Category.Id,
				Name = product.Category.Name,
				GoogleIcon = product.Category.GoogleIcon
			},
			EstimatedCostPrice = product.EstimatedCostPrice,
			CurrentStock = product.CurrentStock,
			StockAlert = product.StockAlert,
			Status = product.Status,
			Observations = product.Observations
		};

		[HttpGet]
		[Authorize(Policy = "products.list")]
		public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll([FromQuery] string? name, [FromQuery] string? code, [FromQuery] int? categoryId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10) {
			var list = await _productService.GetProductsAsync(name ?? string.Empty, categoryId, pageNumber, pageSize);
			if (!string.IsNullOrWhiteSpace(code))
				list = list.Where(product => product.InternalCode.Contains(code));
			return Ok(list.Select(ToDto));
		}

		[HttpGet("{id}")]
		[Authorize(Policy = "products.view")]
		public async Task<ActionResult<ProductDto>> GetById(int id) {
			var product = await _productService.GetProductByIdAsync(id);
			if (product == null) return NotFound();
			return Ok(ToDto(product));
		}

		[HttpPost]
		[Authorize(Policy = "products.create")]
		public async Task<ActionResult<ProductDto>> Create([FromBody] CreateProductDto dto) {
			if (!ModelState.IsValid) return BadRequest(ModelState);
			var product = await _productService.CreateProductAsync(dto);
			return CreatedAtAction(nameof(GetById), new { id = product.Id }, ToDto(product));
		}

		[HttpPut("{id}")]
		[Authorize(Policy = "products.update")]
		public async Task<ActionResult<ProductDto>> Update(int id, [FromBody] UpdateProductDto dto) {
			if (!ModelState.IsValid) return BadRequest(ModelState);
			var product = await _productService.UpdateProductAsync(id, dto);
			return Ok(ToDto(product));
		}

		[HttpDelete("{id}")]
		[Authorize(Policy = "products.delete")]
		public async Task<IActionResult> Delete(int id) {
			var ok = await _productService.DeleteProductAsync(id);
			if (!ok) return NotFound(new { message = "Product not found" });
			return NoContent();
		}

		[HttpGet("categories")]
		[Authorize(Policy = "products.category.list")]
		public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetCategories() {
			var categories = await _productService.GetCategoriesAsync();
			return Ok(categories.Select(categories => new ProductCategoryDto { Id = categories.Id, Name = categories.Name, GoogleIcon = categories.GoogleIcon }));
		}

		[HttpPost("categories")]
		[Authorize(Policy = "products.category.create")]
		public async Task<ActionResult<ProductCategoryDto>> CreateCategory([FromBody] CreateProductCategoryDto dto) {
			if (!ModelState.IsValid) return BadRequest(ModelState);
			var categories = await _productService.CreateCategoryAsync(dto);
			return CreatedAtAction(nameof(GetCategories),
				new { id = categories.Id },
				new ProductCategoryDto { Id = categories.Id, Name = categories.Name, GoogleIcon = categories.GoogleIcon });
		}

		[HttpDelete("categories/{id}")]
		[Authorize(Policy = "products.category.delete")]
		public async Task<IActionResult> DeleteCategory(int id) {
			var exists = await _productService.GetCategoryByIdAsync(id);
			if (exists == null) return NotFound(new { message = "Category not found" });
			var ok = await _productService.DeleteCategoryAsync(id);
			if (!ok) return Conflict(new { message = "Category has associated products" });
			return NoContent();
		}

		[HttpGet("{id}/stock-reserved")]
		[Authorize(Policy = "products.stock.reserved")]
		public async Task<ActionResult<int>> GetReservedStock(int id) {
			var product = await _productService.GetProductByIdAsync(id);
			if (product == null) return NotFound();
			var reserved = await _productService.GetReservedStockAsync(id);
			return Ok(reserved);
		}

		[HttpGet("{id}/stock-available")]
		[Authorize(Policy = "products.stock.available")]
		public async Task<ActionResult<int>> GetAvailableStock(int id) {
			var product = await _productService.GetProductByIdAsync(id);
			if (product == null) return NotFound();
			var avail = await _productService.GetAvailableStockAsync(id);
			return Ok(avail);
		}

		[HttpGet("{id}/stock-real")]
		[Authorize(Policy = "products.stock.real")]
		public async Task<ActionResult<int>> GetRealStock(int id) {
			var product = await _productService.GetProductByIdAsync(id);
			if (product == null) return NotFound();
			var real = await _productService.GetRealStockAsync(id);
			return Ok(real);
		}
	}
}
