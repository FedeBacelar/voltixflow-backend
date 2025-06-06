using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using VoltixFlowAPI.Data;
using VoltixFlowAPI.Filters;
using VoltixFlowAPI.Models;
using VoltixFlowAPI.Repositories;
using VoltixFlowAPI.Repositories.Interfaces;
using VoltixFlowAPI.Services;
using VoltixFlowAPI.Services.Interfaces;

namespace VoltixFlowAPI.Extensions {
	public static class ServiceCollectionExtensions {
		public static IServiceCollection AddDatabase(this IServiceCollection services, string connStr) {
			services.AddDbContext<VoltixDbContext>(opts =>
				opts.UseMySql(connStr, ServerVersion.AutoDetect(connStr)));
			return services;
		}

		public static IServiceCollection AddRepositories(this IServiceCollection services) {
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IRoleRepository, RoleRepository>();
			services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();

			services.AddScoped<IClientRepository, ClientRepository>();
			services.AddScoped<IClientTypeRepository, ClientTypeRepository>();

			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();

			services.AddScoped<IDeliveryNoteItemRepository, DeliveryNoteItemRepository>();

			services.AddScoped<IDeliveryNoteRepository, DeliveryNoteRepository>();
			services.AddScoped<IDeliveryNoteStatusRepository, DeliveryNoteStatusRepository>();

			return services;
		}

		public static IServiceCollection AddDomainServices(this IServiceCollection services) {
			services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IClientService, ClientService>();
			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<IDeliveryNoteService, DeliveryNoteService>();
			services.AddScoped<IAuthService, AuthService>();

			return services;
		}

		public static IServiceCollection AddWebApiConfig(this IServiceCollection services) {
			services
				.AddControllers(opts => opts.Filters.Add<DomainExceptionFilter>())
				.AddJsonOptions(o => {
					o.JsonSerializerOptions.DefaultIgnoreCondition =
						System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
				});
			return services;
		}

		public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services) {
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen(c => {
				c.SwaggerDoc("v1", new OpenApiInfo {
					Title = "VoltixFlow API",
					Version = "v1",
					Description = "API interna para la gestión de Voltix Mayorista"
				});

				var securityScheme = new OpenApiSecurityScheme {
					Name = "Authorization",
					Description = "Ingrese el token JWT con el prefijo: Bearer {token}",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.Http,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					Reference = new OpenApiReference {
						Type = ReferenceType.SecurityScheme,
						Id = "Bearer"
					}
				};
				c.AddSecurityDefinition("Bearer", securityScheme);

				var securityRequirement = new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id   = "Bearer"
							}
						},
						new List<string>()
                    }
				};
				c.AddSecurityRequirement(securityRequirement);
			});

			return services;
		}
	}
}
