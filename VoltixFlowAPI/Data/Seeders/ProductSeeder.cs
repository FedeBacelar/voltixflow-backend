using VoltixFlowAPI.Models;

namespace VoltixFlowAPI.Data.Seeders {
	public static class ProductSeeder {
		public static void Seed(VoltixDbContext context) {
			if (!context.Products.Any()) {
				var tvCat = context.ProductCategories.First(c => c.Name == "Televisores");
				var cellCat = context.ProductCategories.First(c => c.Name == "Celulares");

				var products = new[]
				{
					new Product {
						InternalCode      = "TV-001",
						Name              = "Televisor Samsung 55\"",
						Description       = "Smart TV 4K UHD",
						CategoryId        = tvCat.Id,
						EstimatedCostPrice= 800.00m,
						CurrentStock      = 50,
						StockAlert        = 10,
						Status            = true,
						Observations      = "Modelo demo para pruebas"
					},
					new Product {
						InternalCode      = "CL-001",
						Name              = "Celular iPhone 14",
						Description       = "128GB – Negro",
						CategoryId        = cellCat.Id,
						EstimatedCostPrice= 900.00m,
						CurrentStock      = 30,
						StockAlert        = 5,
						Status            = true,
						Observations      = "Stock inicial de prueba"
					}
				};

				context.Products.AddRange(products);
				context.SaveChanges();
			}
		}
	}
}
