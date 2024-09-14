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
	public class RefreshTokenRequestHandler : IRequestHandler<RefreshTokenRequest, Result<Tokens>>
	{
		ITokenService _service;

		public RefreshTokenRequestHandler(ITokenService service)
		{
			_service = service;
		}

		public async Task<Result<Tokens>> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
		{
			if (request == null || string.IsNullOrEmpty(request.RefreshToken) || request.UserId == 0)
				return await Result<Tokens>.FailureAsync("Missing refresh token details");

			var validateRefreshTokenResponse = await _service.ValidateRefreshTokenAsync(request);

			if (!validateRefreshTokenResponse.Succeeded)
				return await Result<Tokens>.FailureAsync();

			var tokenResponse = await _service.GenerateTokensAsync(validateRefreshTokenResponse.Data);

			return await Result<Tokens>.SuccessAsync(tokenResponse, "Tokens wwere refreshed successfully");
		}
	}
}
