using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace VoltixFlowAPI.Authorization {

	public class PermissionPolicyProvider : DefaultAuthorizationPolicyProvider {
		public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
			: base(options) {
		}

		public override Task<AuthorizationPolicy?> GetPolicyAsync(string policyName) {
			var policy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
				.RequireClaim("permission", policyName)
				.Build();

			return Task.FromResult<AuthorizationPolicy?>(policy);
		}
	}
}
