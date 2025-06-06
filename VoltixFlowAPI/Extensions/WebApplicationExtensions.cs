using Microsoft.EntityFrameworkCore;
using VoltixFlowAPI.Data;

namespace VoltixFlowAPI.Extensions {
	public static class WebApplicationExtensions {
		public static WebApplication MigrateDatabase(this WebApplication app) {
			using var scope = app.Services.CreateScope();
			var db = scope.ServiceProvider.GetRequiredService<VoltixDbContext>();
			db.Database.Migrate();
			return app;
		}
	}
}
