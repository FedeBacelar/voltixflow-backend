namespace VoltixFlowAPI.Data.Seeders
{
    public static class DbSeeder
    {
        public static void Seed(VoltixDbContext context, IServiceProvider services)
        {
			PermissionSeeder.Seed(context);
			RoleSeeder.Seed(context);
			RolePermissionSeeder.Seed(context);
			ClientTypeSeeder.Seed(context);
            UserSeeder.Seed(context, services);

            ProductCategorySeeder.Seed(context);
            ProductSeeder.Seed(context);
            ClientSeeder.Seed(context);
            DeliveryNoteStatusSeeder.Seed(context);
            DeliveryNoteSeeder.Seed(context);
        }
    }
}
