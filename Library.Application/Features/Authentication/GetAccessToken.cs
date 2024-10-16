using Library.Application.DTOs;
using Library.Application.Interfaces.Services;
using Library.Shared.Results;
using MediatR;

namespace Library.Application.Features.Authentication
{
	public class GetAccessToken : TokenDTO, IRequest<Result<string>> { }
	public class AccessTokenHandler : IRequestHandler<GetAccessToken, Result<string>>
	{
		readonly ITokenService _service;

		public AccessTokenHandler(ITokenService service)
		{
			_service = service;
		}

		public async Task<Result<string>> Handle(GetAccessToken request, CancellationToken cancellationToken)
		{
			if (request == null || string.IsNullOrEmpty(request.Token) || request.UserId == 0)
				return await Result<string>.FailureAsync("Missing refresh token details");

			var validateRefreshTokenResponse = await _service.ValidateRefreshTokenAsync(request);

			if (!validateRefreshTokenResponse.Succeeded)
				return await Result<string>.FailureAsync(validateRefreshTokenResponse.Messages);

			var tokenResponse = await _service.GenerateAccessTokenAsync(validateRefreshTokenResponse.Data);

			return await Result<string>.SuccessAsync(tokenResponse, "Token was refreshed successfully");
		}
	}
}
