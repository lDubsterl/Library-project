using Library.Shared.Results;
using Library.Domain.Requests;
using Library.Domain.Entities;

namespace Library.Application.Interfaces.Services
{
	public interface IAuthenticationService
	{
		Task<Result<Tokens>> LoginAsync(LoginRequest request);
		Task<Result<string>> SignUpAsync(SignUpRequest request);
		Task<Result<bool>> LogoutAsync(int userId);
	}
}
