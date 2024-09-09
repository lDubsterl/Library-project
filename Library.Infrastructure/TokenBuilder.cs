﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure
{
	public class TokenBuilder
	{
		public const string Issuer = "LibraryAPI";
		public const string Audience = "LibraryUser";
		public const string Secret = "saujfhaclni67423o17typ239398I&^%B&^I0p98/,'d;fiйцщ378н3з09зщшщцуфрадфлцоург'";
		public static async Task<string> GenerateAccessToken(int userId)
		{
			var tokenHandler = new JwtSecurityTokenHandler();

			var key = Convert.FromBase64String(Secret);
			var claimsIdentity = new ClaimsIdentity([new Claim(ClaimTypes.NameIdentifier, userId.ToString())]);
			var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = claimsIdentity,
				Issuer = Issuer,
				Audience = Audience,
				Expires = DateTime.UtcNow.AddMinutes(15),
				SigningCredentials = signingCredentials
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return await Task.Run(() => tokenHandler.WriteToken(token));
		}
		public static async Task<string> GenerateRefreshToken()
		{
			var randomBytes = new byte[32];
			var random = RandomNumberGenerator.Create();
			await Task.Run(() => random.GetBytes(randomBytes));
			return Convert.ToBase64String(randomBytes);
		}
	}
}
