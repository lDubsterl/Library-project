using Library.Application.Interfaces.Services;
using Library.Application.Requests;
using Library.Shared.Results;
using MediatR;

namespace Library.Application.Features.Authentication
{
	public class SignUpHandler: IRequestHandler<SignUp, Result<string>>
	{
		IAuthenticationService _service;

		public SignUpHandler(IAuthenticationService service)
		{
			_service = service;
		}

		public async Task<Result<string>> Handle(SignUp request, CancellationToken cancellationToken)
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
