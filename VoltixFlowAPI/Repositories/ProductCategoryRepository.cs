using Microsoft.EntityFrameworkCore;
using VoltixFlowAPI.Data;
using VoltixFlowAPI.Models;
using VoltixFlowAPI.Repositories.Interfaces;

namespace VoltixFlowAPI.Repositories {
	public class ProductCategoryRepository : IProductCategoryRepository {
		private readonly VoltixDbContext _context;
		public ProductCategoryRepository(VoltixDbContext context) => _context = context;

		public async Task<IEnumerable<ProductCategory>> GetAllAsync()
			=> await _context.ProductCategories.ToListAsync();

		public async Task<ProductCategory?> GetByIdAsync(int id)
			=> await _context.ProductCategories.FindAsync(id);

		public async Task<ProductCategory> CreateAsync(ProductCategory category) {
			_context.ProductCategories.Add(category);
			await _context.SaveChangesAsync();
			return category;
		}

		public async Task<bool> DeleteAsync(int id) {
			var category = await _context.ProductCategories.FindAsync(id);
			if (category == null) return false;

			_context.ProductCategories.Remove(category);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
