using Library.Application.Interfaces.Services;
using Library.Domain.Entities;
using Library.Domain.Requests;
using Library.Persistence.Contexts;
using Library.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly LibraryDbContext _db;
		private readonly ITokenService tokenService;

		public AuthenticationService(LibraryDbContext tasksDbContext, ITokenService tokenService)
		{
			this._db = tasksDbContext;
			this.tokenService = tokenService;
		}

		public async Task<Result<Tuple<string, string>>> LoginAsync(LoginRequest loginRequest)
		{
			var user = _db.Users.SingleOrDefault(user => user.Email == loginRequest.Email);

			if (user == null)
			{
				return await Result<Tuple<string, string>>.FailureAsync("Email not found");
			}
			var passwordHash = PasswordBuilder.HashUsingPbkdf2(loginRequest.Password, Convert.FromBase64String(user.PasswordSalt));

			if (user.Password != passwordHash)
			{
				return await Result<Tuple<string, string>>.FailureAsync("Invalid password");
			}

			var token = await Task.Run(() => tokenService.GenerateTokensAsync(user.Id));

			return Result<Tuple<string, string>>.Success(token!);
		}

		public async Task<Result<bool>> LogoutAsync(int userId)
		{
			var refreshToken = await _db.RefreshTokens.FirstOrDefaultAsync(o => o.UserId == userId);

			if (refreshToken == null)
			{
				return await Result<bool>.SuccessAsync();
			}

			_db.RefreshTokens.Remove(refreshToken);

			var saveResponse = await _db.SaveChangesAsync();

			if (saveResponse >= 0)
			{
				return await Result<bool>.SuccessAsync();
			}

			return Result<bool>.Failure("Unable to logout user");

		}

		public async Task<Result<string>> SignUpAsync(SignUpRequest signupRequest)
		{
			var existingUser = await _db.Users.SingleOrDefaultAsync(user => user.Email == signupRequest.Email);

			if (existingUser != null)
			{
				return await Result<string>.FailureAsync("User already exists with the same email");
			}

			if (signupRequest.Password != signupRequest.ConfirmPassword)
			{
				return await Result<string>.FailureAsync("Password and confirm password do not match");
			}

			if (signupRequest.Password.Length <= 7)
			{
				return await Result<string>.FailureAsync("Password is weak");
			}

			var salt = PasswordBuilder.GetSecureSalt();
			var passwordHash = PasswordBuilder.HashUsingPbkdf2(signupRequest.Password, salt);

			var user = new User
			{
				Email = signupRequest.Email,
				Password = passwordHash,
				PasswordSalt = Convert.ToBase64String(salt),
				Ts = signupRequest.Ts
			};

			await _db.Users.AddAsync(user);

			var saveResponse = await _db.SaveChangesAsync();

			if (saveResponse >= 0)
			{
				return await Result<string>.SuccessAsync(user.Email);
			}
			return await Result<string>.FailureAsync("Unable to save the user");
		}
	}
}
