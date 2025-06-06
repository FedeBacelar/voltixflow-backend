using Microsoft.EntityFrameworkCore;
using VoltixFlowAPI.Models;

namespace VoltixFlowAPI.Data {
	public class VoltixDbContext : DbContext {
		public VoltixDbContext(DbContextOptions<VoltixDbContext> options)
			: base(options) {
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<Permission> Permissions { get; set; }
		public DbSet<RolePermission> RolePermissions { get; set; }

		public DbSet<Client> Clients { get; set; }
		public DbSet<ClientType> ClientTypes { get; set; }

		public DbSet<Product> Products { get; set; }
		public DbSet<ProductCategory> ProductCategories { get; set; }

		public DbSet<DeliveryNote> DeliveryNotes { get; set; }
		public DbSet<DeliveryNoteItem> DeliveryNoteItems { get; set; }
		public DbSet<DeliveryNoteStatus> DeliveryNoteStatuses { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Product>()
				.HasOne(p => p.Category)
				.WithMany(c => c.Products)
				.HasForeignKey(p => p.CategoryId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
