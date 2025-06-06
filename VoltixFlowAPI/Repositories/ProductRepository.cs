using Microsoft.EntityFrameworkCore;
using VoltixFlowAPI.Data;
using VoltixFlowAPI.Models;
using VoltixFlowAPI.Repositories.Interfaces;

namespace VoltixFlowAPI.Repositories {
	public class ProductRepository : IProductRepository {
		private readonly VoltixDbContext _context;
		public ProductRepository(VoltixDbContext context) => _context = context;

		public async Task<Product> CreateAsync(Product product) {
			_context.Products.Add(product);
			await _context.SaveChangesAsync();
			return product;
		}

		public Task<Product?> GetByIdAsync(int id) =>
			_context.Products
					.Include(product => product.Category)
					.FirstOrDefaultAsync(product => product.Id == id);

		public async Task<IEnumerable<Product>> GetAllAsync(string? name, int? categoryId, int pageNumber, int pageSize) {
			var q = _context.Products
							.Include(p => p.Category)
							.Where(p => p.Status)
							.AsQueryable();
			if (!string.IsNullOrWhiteSpace(name))
				q = q.Where(product => product.Name.Contains(name));
			if (categoryId.HasValue)
				q = q.Where(product => product.CategoryId == categoryId.Value);

			return await q
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();
		}

		public async Task<Product> UpdateAsync(int id, Product updatedProduct) {
			var product = await _context.Products.FindAsync(id)
						 ?? throw new KeyNotFoundException("Product not found");

			product.InternalCode = updatedProduct.InternalCode;
			product.Name = updatedProduct.Name;
			product.Description = updatedProduct.Description;
			product.CategoryId = updatedProduct.CategoryId;
			product.EstimatedCostPrice = updatedProduct.EstimatedCostPrice;
			product.CurrentStock = updatedProduct.CurrentStock;
			product.StockAlert = updatedProduct.StockAlert;
			product.Status = updatedProduct.Status;
			product.Observations = updatedProduct.Observations;

			await _context.SaveChangesAsync();
			return product;
		}

		public async Task<bool> DeleteAsync(int id) {
			var product = await _context.Products.FindAsync(id);
			if (product == null)
				return false;

			product.Status = false;
			product.CurrentStock = 0;

			await _context.SaveChangesAsync();
			return true;
		}
	}
}
