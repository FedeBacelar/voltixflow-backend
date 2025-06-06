using VoltixFlowAPI.Models;

namespace VoltixFlowAPI.Data.Seeders {
	public static class ProductCategorySeeder {
		public static void Seed(VoltixDbContext context) {
			if (!context.ProductCategories.Any()) {
				context.ProductCategories.AddRange(new[]
				{
					new ProductCategory { Name = "Televisores", GoogleIcon = "tv" },
					new ProductCategory { Name = "Celulares",   GoogleIcon = "smartphone" }
				});
				context.SaveChanges();
			}
		}
	}
}
