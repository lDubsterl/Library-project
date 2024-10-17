using Library.Application.AuthenticationRequests;
using Library.Application.Interfaces.Services;
using Library.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.Application.Features.Authentication
{
	public class SignUpHandler: IRequestHandler<SignUp, IActionResult>
	{
		IAuthenticationService _service;

		public SignUpHandler(IAuthenticationService service)
		{
			_service = service;
		}

		public async Task<IActionResult> Handle(SignUp request, CancellationToken cancellationToken)
		{
			var signupResponse = await _service.SignUpAsync(request);

			if (signupResponse is BadRequestObjectResult fail)
			{
				return fail;
			}

			return new OkResult();
		}
	}
}
