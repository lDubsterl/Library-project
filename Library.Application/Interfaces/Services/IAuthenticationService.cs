using Library.Shared.Results;
using Library.Domain.Requests;

namespace Library.Application.Interfaces.Services
{
	public interface IAuthenticationService
	{
		Task<Result<Tuple<string, string>>> LoginAsync(LoginRequest request);
		Task<Result<string>> SignUpAsync(SignUpRequest request);
		Task<Result<bool>> LogoutAsync(int userId);
	}
}
