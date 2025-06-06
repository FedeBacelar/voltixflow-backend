using VoltixFlowAPI.Extensions;
using VoltixFlowAPI.Data.Seeders;
using VoltixFlowAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);
var conn = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services
	.AddDatabase(conn)
	.AddRepositories()
	.AddDomainServices()
	.AddWebApiConfig()
	.AddSwaggerDocumentation();

builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, CustomAuthorizationMiddlewareResultHandler>();

using (var sp = builder.Services.BuildServiceProvider())
using (var scope = sp.CreateScope()) {
	var contexto = scope.ServiceProvider.GetRequiredService<VoltixDbContext>();
	var todosLosPermisos = contexto.Permissions
		.Select(p => p.Name)
		.ToList();

	builder.Services.AddAuthorization(options => {
		foreach (var perm in todosLosPermisos) {
			options.AddPolicy(perm, policy =>
				policy.RequireClaim("permission", perm));
		}
	});
}

builder.Services.AddCors(options => {
	options.AddPolicy("AllowFrontend", policy => {
		policy.WithOrigins("http://localhost:4200")
			  .AllowAnyMethod()
			  .AllowAnyHeader()
			  .AllowCredentials();
	});
});

builder.Services
	.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options => {
		options.TokenValidationParameters = new TokenValidationParameters {
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,

			ValidIssuer = builder.Configuration["Jwt:Issuer"],
			ValidAudience = builder.Configuration["Jwt:Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
		};
	});

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsStaging()) {
	app.MigrateDatabase();
	using var scope = app.Services.CreateScope();
	var context = scope.ServiceProvider.GetRequiredService<VoltixDbContext>();
	var services = scope.ServiceProvider;
	DbSeeder.Seed(context, services);
}

if (app.Environment.IsDevelopment() || app.Environment.IsStaging()) {
	app.UseSwagger();
	app.UseSwaggerUI(c => {
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "VoltixFlow API v1");
		c.DocumentTitle = "VoltixFlow – API Docs";
	});
} else {
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
