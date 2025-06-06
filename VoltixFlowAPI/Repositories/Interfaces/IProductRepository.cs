using VoltixFlowAPI.Models;

namespace VoltixFlowAPI.Repositories.Interfaces {
	public interface IProductRepository {
		Task<Product> CreateAsync(Product product);
		Task<Product?> GetByIdAsync(int id);
		Task<IEnumerable<Product>> GetAllAsync(string? name, int? categoryId, int pageNumber, int pageSize);
		Task<Product> UpdateAsync(int id, Product updatedProduct);
		Task<bool> DeleteAsync(int id);
	}
}
