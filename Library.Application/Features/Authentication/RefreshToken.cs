using Library.Application.DTOs;
using Library.Application.Interfaces.Services;
using Library.Application.Requests;
using Library.Shared.Results;
using MediatR;

namespace Library.Application.Features.Authentication
{
	public class RefreshToken : TokenDTO, IRequest<Result<Tokens>> { }
	public class RefreshTokenHandler : IRequestHandler<RefreshToken, Result<Tokens>>
	{
		ITokenService _service;

		public RefreshTokenHandler(ITokenService tokenService)
		{
			this._service = tokenService;
		}

		public async Task<Result<Tokens>> Handle(RefreshToken request, CancellationToken cancellationToken)
		{
			if (request == null || string.IsNullOrEmpty(request.Token) || request.UserId == 0)
				return await Result<Tokens>.FailureAsync("Missing refresh token details");

			var validateRefreshTokenResponse = await _service.ValidateRefreshTokenAsync(request);

			if (!validateRefreshTokenResponse.Succeeded)
				return await Result<Tokens>.FailureAsync(validateRefreshTokenResponse.Messages);

			var tokenResponse = await _service.GenerateTokensAsync(validateRefreshTokenResponse.Data);

			return await Result<Tokens>.SuccessAsync(tokenResponse, "Tokens were refreshed successfully");
		}
	}
}
