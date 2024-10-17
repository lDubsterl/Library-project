using Library.Application.AuthenticationRequests;
using Microsoft.AspNetCore.Mvc;

namespace Library.Application.Interfaces.Services
{
	public interface IAuthenticationService
	{
		Task<IActionResult> LoginAsync(Login request);
		Task<IActionResult> SignUpAsync(SignUp request);
		Task<IActionResult> LogoutAsync(int userId);
	}
}
