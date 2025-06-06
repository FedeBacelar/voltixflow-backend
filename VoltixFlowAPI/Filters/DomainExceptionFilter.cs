using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace VoltixFlowAPI.Filters {
	public class DomainExceptionFilter : IAsyncExceptionFilter {
		public Task OnExceptionAsync(ExceptionContext context) {
			switch (context.Exception) {
				case KeyNotFoundException knf:
					context.Result = new NotFoundObjectResult(
						new { message = knf.Message });
					context.ExceptionHandled = true;
					break;

				case InvalidOperationException ioe:
					context.Result = new ObjectResult(
						new { message = ioe.Message }) {
						StatusCode = StatusCodes.Status409Conflict
					};
					context.ExceptionHandled = true;
					break;
			}
			return Task.CompletedTask;
		}
	}
}
