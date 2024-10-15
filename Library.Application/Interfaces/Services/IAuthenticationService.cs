using Library.Shared.Results;
using Library.Application.AuthenticationRequests;

namespace Library.Application.Interfaces.Services
{
    public interface IAuthenticationService
	{
		Task<Result<Tokens>> LoginAsync(Login request);
		Task<Result<string>> SignUpAsync(SignUp request);
		Task<Result<bool>> LogoutAsync(int userId);
	}
}
