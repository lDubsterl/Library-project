using Library.Application.AuthenticationRequests;
using Library.Application.DTOs;
using Library.Domain.Entities;
using Library.Shared.Results;

namespace Library.Application.Interfaces.Services
{
    public interface ITokenService
	{
		Task<string> GenerateAccessTokenAsync(int userId);
		Task<Tokens> GenerateTokensAsync(int userId);
		Task<Result<int>> ValidateRefreshTokenAsync(TokenDTO refreshTokenRequest);
		Task<bool> RemoveRefreshTokenAsync(User user);
	}
}
