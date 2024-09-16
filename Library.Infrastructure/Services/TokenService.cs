﻿using Library.Application.DTOs;
using Library.Application.Interfaces.Services;
using Library.Domain.Common;
using Library.Domain.Entities;
using Library.Persistence.Contexts;
using Library.Shared.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Library.Infrastructure.Services
{
	public class TokenService : ITokenService
	{
		readonly LibraryDbContext _db;

		public TokenService(LibraryDbContext db)
		{
			_db = db;
		}

		public async Task<string> GenerateAccessTokenAsync(int userId)
		{
			var userRecord = await _db.Users.Include(t => t.RefreshTokens).SingleAsync(u => u.Id == userId);
			
			if (userRecord == null)
				return null;

			var accessToken = await TokenBuilder.GenerateAccessToken(userId, userRecord.Role);
			return accessToken;
		}

		public async Task<Tokens> GenerateTokensAsync(int userId)
		{
			var userRecord = await _db.Users.Include(t => t.RefreshTokens).SingleAsync(u => u.Id == userId);

			if (userRecord == null)
				return null;

			var accessToken = await TokenBuilder.GenerateAccessToken(userId, userRecord.Role);
			var refreshToken = await TokenBuilder.GenerateRefreshToken();

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

			var token = new Tokens { AccessToken = accessToken, RefreshToken = refreshToken };

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

		public async Task<Result<int>> ValidateRefreshTokenAsync(TokenDTO refreshTokenRequest)
		{
			var refreshToken = await _db.RefreshTokens.FirstOrDefaultAsync(o => o.UserId == refreshTokenRequest.UserId);

			if (refreshToken == null)
				return await Result<int>.FailureAsync("Invalid session or user is already logged out");

			var refreshTokenToValidateHash = PasswordBuilder.HashUsingPbkdf2(refreshTokenRequest.Token, Convert.FromBase64String(refreshToken.TokenSalt));

			if (refreshToken.TokenHash != refreshTokenToValidateHash)
				return await Result<int>.FailureAsync("Invalid refresh token");

			if (refreshToken.ExpiryDate < DateTime.UtcNow)
				return await Result<int>.FailureAsync("Refresh token has expired");

			return await Result<int>.SuccessAsync(refreshToken.UserId);
		}
	}
}
