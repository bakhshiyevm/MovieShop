using DTO;
using Helper.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Text.Json;

namespace API.Middlewares
{
	public class ExceptionMiddleware
	{
		private class ExceptionDTO 
		{
			public string Message { get; set; }
			//public int? Code { get; set; }		

			public ExceptionDTO(string message)
			{
				Message = message;			}
		}

		private readonly RequestDelegate _next;
		private HttpContext _httpContext;

		public ExceptionMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				_httpContext = httpContext;
				await _next(httpContext);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(HttpStatusCode.InternalServerError,
					 new ExceptionDTO(ex.Message));
			}
		}

		private async Task HandleExceptionAsync<T>(HttpStatusCode code, T ex)
		{
			_httpContext.Response.ContentType = "application/json";
			_httpContext.Response.StatusCode = (int)code;

			var resp = new Response<T>(ex);

			await _httpContext.Response.WriteAsync(resp.SerializeJson(true));
		}
	}
}
