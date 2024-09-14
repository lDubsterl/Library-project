using Library.Domain.Entities;
using Library.Domain.Requests;
using Library.Shared.Results;

namespace Library.Application.Interfaces.Services
{
	public interface ITokenService
	{
		Task<Tokens> GenerateTokensAsync(int userId);
		Task<Result<int>> ValidateRefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
		Task<bool> RemoveRefreshTokenAsync(User user);
	}
}
