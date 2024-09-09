using Library.Application.Interfaces.Services;
using Library.Domain.Entities;
using Library.Domain.Requests;
using Library.Persistence.Contexts;
using Library.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Services
{
	internal class TokenService : ITokenService
	{
		readonly LibraryDbContext _db;

		public TokenService(LibraryDbContext db)
		{
			_db = db;
		}

		public async Task<Tuple<string, string>?> GenerateTokensAsync(int userId)
		{
			var accessToken = await TokenBuilder.GenerateAccessToken(userId);
			var refreshToken = await TokenBuilder.GenerateRefreshToken();

			var userRecord = await _db.Users.Include(o => o.RefreshTokens).FirstOrDefaultAsync(e => e.Id == userId);

			if (userRecord == null)
			{
				return null;
			}

			var salt = PasswordBuilder.GetSecureSalt();

			var refreshTokenHashed = PasswordBuilder.HashUsingPbkdf2(refreshToken, salt);

			if (userRecord.RefreshTokens != null && userRecord.RefreshTokens.Count != 0)
			{
				await RemoveRefreshTokenAsync(userRecord);

			}
			userRecord.RefreshTokens?.Add(new RefreshToken
			{
				ExpiryDate = DateTime.UtcNow.AddDays(30),
				Ts = DateTime.UtcNow,
				UserId = userId,
				TokenHash = refreshTokenHashed,
				TokenSalt = Convert.ToBase64String(salt)

			});

			await _db.SaveChangesAsync();

			var token = (accessToken, refreshToken).ToTuple();

			return token;
		}

		public async Task<bool> RemoveRefreshTokenAsync(User user)
		{
			var userRecord = await _db.Users.Include(o => o.RefreshTokens).FirstOrDefaultAsync(e => e.Id == user.Id);

			if (userRecord == null)
			{
				return false;
			}

			if (userRecord.RefreshTokens != null && userRecord.RefreshTokens.Count != 0)
			{
				var currentRefreshToken = userRecord.RefreshTokens.First();

				_db.RefreshTokens.Remove(currentRefreshToken);
			}

			return false;
		}

		public async Task<Result<int>> ValidateRefreshTokenAsync(RefreshTokenRequest refreshTokenRequest)
		{
			var refreshToken = await _db.RefreshTokens.FirstOrDefaultAsync(o => o.UserId == refreshTokenRequest.UserId);

			var response = new Result<int>();
			if (refreshToken == null)
			{
				response.Succeeded = false;
				response.Messages.Add("Invalid session or user is already logged out");
				response.Code = 401;
				return response;
			}

			var refreshTokenToValidateHash = PasswordBuilder.HashUsingPbkdf2(refreshTokenRequest.RefreshToken, Convert.FromBase64String(refreshToken.TokenSalt));

			if (refreshToken.TokenHash != refreshTokenToValidateHash)
			{
				response.Succeeded = false;
				response.Messages.Add("Invalid refresh token");
				response.Code = 401;
				return response;
			}

			if (refreshToken.ExpiryDate < DateTime.UtcNow)
			{
				response.Succeeded = false;
				response.Messages.Add("Refresh token has expired");
				response.Code = 401;
				return response;
			}

			response.Succeeded = true;
			response.Data = refreshToken.UserId;

			return response;
		}
	}
}
