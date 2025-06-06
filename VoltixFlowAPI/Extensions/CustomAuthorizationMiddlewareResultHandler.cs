using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;


namespace VoltixFlowAPI.Extensions {
	public class CustomAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler {
		private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new();

		public async Task HandleAsync(RequestDelegate next,HttpContext context,AuthorizationPolicy policy,PolicyAuthorizationResult authorizeResult) {
			if (authorizeResult.Forbidden) {
				var endpoint = context.GetEndpoint();
				if (endpoint != null) {
					var policiesSolicitadas = endpoint.Metadata
						.GetOrderedMetadata<AuthorizeAttribute>()
						.Where(a => !string.IsNullOrEmpty(a.Policy))
						.Select(a => a.Policy)
						.Distinct()
						.ToList();

					var permisosRequeridos = policiesSolicitadas.Any()
						? string.Join(", ", policiesSolicitadas)
						: "(desconocido)";

					context.Response.StatusCode = StatusCodes.Status403Forbidden;
					context.Response.ContentType = "application/json";
					await context.Response.WriteAsJsonAsync(new {
						message = $"Te falta el/los siguiente(s) permiso(s): {permisosRequeridos}"
					});
					return;
				}
			}

			if (authorizeResult.Challenged) {
				context.Response.StatusCode = StatusCodes.Status401Unauthorized;
				context.Response.ContentType = "application/json";
				await context.Response.WriteAsJsonAsync(new {
					message = "No estás autenticado. Por favor inicia sesión."
				});
				return;
			}

			await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
		}
	}
}
