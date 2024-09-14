using Library.Application.Interfaces.Services;
using Library.Domain.Requests;
using Library.Shared.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Features.Authentication
{
	public class SignUpHandler: IRequestHandler<SignUpRequest, Result<string>>
	{
		IAuthenticationService _service;

		public SignUpHandler(IAuthenticationService service)
		{
			_service = service;
		}

		public async Task<Result<string>> Handle(SignUpRequest request, CancellationToken cancellationToken)
		{
			var signupResponse = await _service.SignUpAsync(request);

			if (!signupResponse.Succeeded)
			{
				return await Result<string>.FailureAsync(signupResponse.Messages);
			}

			return await Result<string>.SuccessAsync(request.Email);
		}
	}
}
