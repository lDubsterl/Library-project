using Library.Application.AuthenticationRequests;
using Library.Application.DTOs;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Library.Application.Interfaces.Services
{
	public interface ITokenService
	{
		Task<string> GenerateAccessTokenAsync(int userId);
		Task<Tokens> GenerateTokensAsync(int userId);
		Task<object> ValidateRefreshTokenAsync(TokenDTO refreshTokenRequest);
		Task<bool> RemoveRefreshTokenAsync(User user);
	}
}
