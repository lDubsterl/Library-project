using Library.Application.AuthenticationRequests;
using Library.Application.DTOs;
using Library.Application.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.Application.Features.Authentication
{
	public class RefreshToken : TokenDTO, IRequest<IActionResult> { }
	public class RefreshTokenHandler : IRequestHandler<RefreshToken, IActionResult>
	{
		ITokenService _service;

		public RefreshTokenHandler(ITokenService tokenService)
		{
			this._service = tokenService;
		}

		public async Task<IActionResult> Handle(RefreshToken request, CancellationToken cancellationToken)
		{
			if (request == null || string.IsNullOrEmpty(request.Token) || request.UserId == 0)
				return new BadRequestObjectResult(new { Message = "Missing refresh token details" });

			var validateRefreshTokenResponse = await _service.ValidateRefreshTokenAsync(request);

			if (validateRefreshTokenResponse is BadRequestResult fail)
				return fail;

			var userId = (int)validateRefreshTokenResponse;
			var tokenResponse = await _service.GenerateTokensAsync(userId);

			return new OkObjectResult(new { Data = tokenResponse, Message = "Tokens were refreshed successfully" });
		}
	}
}
