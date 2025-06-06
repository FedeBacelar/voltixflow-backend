using VoltixFlowAPI.Models;

namespace VoltixFlowAPI.Repositories.Interfaces {
	public interface IProductCategoryRepository {
		Task<IEnumerable<ProductCategory>> GetAllAsync();
		Task<ProductCategory?> GetByIdAsync(int id);
		Task<ProductCategory> CreateAsync(ProductCategory category);
		Task<bool> DeleteAsync(int id);
	}
}
