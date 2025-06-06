using VoltixFlowAPI.Dtos;
using VoltixFlowAPI.Models;

namespace VoltixFlowAPI.Services.Interfaces {
	public interface IProductService {
		Task<Product> CreateProductAsync(CreateProductDto dto);
		Task<Product?> GetProductByIdAsync(int id);
		Task<IEnumerable<Product>> GetProductsAsync(string? name, int? categoryId, int pageNumber, int pageSize);
		Task<Product> UpdateProductAsync(int id, UpdateProductDto dto);
		Task<bool> DeleteProductAsync(int id);
		Task<ProductCategory> CreateCategoryAsync(CreateProductCategoryDto dto);
		Task<bool> DeleteCategoryAsync(int id);
		Task<ProductCategory?> GetCategoryByIdAsync(int id);
		Task<IEnumerable<ProductCategory>> GetCategoriesAsync();
		Task<int> GetReservedStockAsync(int productId);
		Task<int> GetAvailableStockAsync(int productId);
		Task<int> GetRealStockAsync(int productId);
	}
}
