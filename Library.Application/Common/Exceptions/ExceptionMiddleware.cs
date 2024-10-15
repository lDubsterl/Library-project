using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace Library.Application.Common.Exceptions
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionMiddleware(RequestDelegate next)
		{
			_next = next;
		}
		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (ValidationException ex)
			{
				await HandleExceptionAsync(context, ex);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}
		private static Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/problem+json";
			int statusCode = (int)HttpStatusCode.InternalServerError;
			string error = "";

			if (exception is ValidationException ex)
			{
				statusCode = (int)HttpStatusCode.BadRequest;
				List<object> Errors = [];
				foreach (var exError in ex.Errors)
				{
					Errors.Add(new ValidationErrorResponse(exError));
				}
				error = JsonSerializer.Serialize(
					new
					{
						Errors
					});
			}
			context.Response.StatusCode = statusCode;
			return context.Response.WriteAsync(error);
		}
	}
}
