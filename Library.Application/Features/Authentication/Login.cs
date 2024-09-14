using Library.Application.Interfaces.Services;
using Library.Domain.Entities;
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

	public class LoginHandler : IRequestHandler<LoginRequest, Result<Tokens>>
	{
		IAuthenticationService _service;
		public LoginHandler(IAuthenticationService service)
		{
			_service = service;
		}
		public async Task<Result<Tokens>> Handle(LoginRequest request, CancellationToken cancellationToken)
		{
			return await _service.LoginAsync(request);
		}
	}
}
