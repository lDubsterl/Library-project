using Library.Application.Interfaces.Services;
using Library.Application.Requests;
using Library.Shared.Results;
using MediatR;

namespace Library.Application.Features.Authentication
{

	public class LoginHandler : IRequestHandler<Login, Result<Tokens>>
	{
		IAuthenticationService _service;
		public LoginHandler(IAuthenticationService service)
		{
			_service = service;
		}
		public async Task<Result<Tokens>> Handle(Login request, CancellationToken cancellationToken)
		{
			return await _service.LoginAsync(request);
		}
	}
}
