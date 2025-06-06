using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VoltixFlowAPI.Dtos;
using VoltixFlowAPI.Models;
using VoltixFlowAPI.Repositories.Interfaces;
using VoltixFlowAPI.Services.Interfaces;

namespace VoltixFlowAPI.Services {
	public class ProductService : IProductService {
		private readonly IProductRepository _productRepo;
		private readonly IProductCategoryRepository _categoryRepo;
		private readonly IDeliveryNoteItemRepository _noteItemRepo;

		public ProductService(IProductRepository productRepo, IProductCategoryRepository categoryRepo, IDeliveryNoteItemRepository noteItemRepo) {
			_productRepo = productRepo;
			_categoryRepo = categoryRepo;
			_noteItemRepo = noteItemRepo;
		}

		public async Task<Product> CreateProductAsync(CreateProductDto dto) {
			if (await _categoryRepo.GetByIdAsync(dto.CategoryId) is null)
				throw new ArgumentException("CategoryId inválido");

			var product = new Product {
				InternalCode = dto.InternalCode,
				Name = dto.Name,
				Description = dto.Description,
				CategoryId = dto.CategoryId,
				EstimatedCostPrice = dto.EstimatedCostPrice,
				CurrentStock = dto.CurrentStock,
				StockAlert = dto.StockAlert,
				Status = dto.Status,
				Observations = dto.Observations ?? string.Empty
			};
			return await _productRepo.CreateAsync(product);
		}

		public Task<Product?> GetProductByIdAsync(int id) =>
			_productRepo.GetByIdAsync(id);

		public Task<IEnumerable<Product>> GetProductsAsync(string? name, int? categoryId, int pageNumber, int pageSize) =>
			_productRepo.GetAllAsync(name, categoryId, pageNumber, pageSize);

		public async Task<Product> UpdateProductAsync(int id, UpdateProductDto dto) {
			var existing = await _productRepo.GetByIdAsync(id)
						   ?? throw new KeyNotFoundException("Product not found");

			if (await _categoryRepo.GetByIdAsync(dto.CategoryId) is null)
				throw new ArgumentException("CategoryId inválido");

			existing.InternalCode = dto.InternalCode;
			existing.Name = dto.Name;
			existing.Description = dto.Description;
			existing.CategoryId = dto.CategoryId;
			existing.EstimatedCostPrice = dto.EstimatedCostPrice;
			existing.CurrentStock = dto.CurrentStock;
			existing.StockAlert = dto.StockAlert;
			existing.Status = dto.Status;
			existing.Observations = dto.Observations ?? string.Empty;

			return await _productRepo.UpdateAsync(id, existing);
		}

		public Task<bool> DeleteProductAsync(int id) =>
			_productRepo.DeleteAsync(id);

		public Task<ProductCategory> CreateCategoryAsync(CreateProductCategoryDto dto) =>
			_categoryRepo.CreateAsync(new ProductCategory {
				Name = dto.Name,
				GoogleIcon = dto.GoogleIcon
			});

		public async Task<bool> DeleteCategoryAsync(int id) {
			var cat = await _categoryRepo.GetByIdAsync(id);
			if (cat == null) return false;

			var hasChildren = (await _productRepo.GetAllAsync(null, id, 1, 1)).Any();
			if (hasChildren) return false;

			return await _categoryRepo.DeleteAsync(id);
		}

		public Task<ProductCategory?> GetCategoryByIdAsync(int id) =>
			_categoryRepo.GetByIdAsync(id);

		public Task<IEnumerable<ProductCategory>> GetCategoriesAsync() =>
			_categoryRepo.GetAllAsync();

		public Task<int> GetReservedStockAsync(int productId) =>
			_noteItemRepo.GetReservedStockAsync(productId);

		public async Task<int> GetAvailableStockAsync(int productId) {
			var prod = await _productRepo.GetByIdAsync(productId)
					   ?? throw new KeyNotFoundException("Product not found");
			return prod.CurrentStock;
		}

		public async Task<int> GetRealStockAsync(int productId) {
			var prod = await _productRepo.GetByIdAsync(productId)
						   ?? throw new KeyNotFoundException("Product not found");
			var reserved = await _noteItemRepo.GetReservedStockAsync(productId);
			return prod.CurrentStock + reserved;
		}
	}
}
