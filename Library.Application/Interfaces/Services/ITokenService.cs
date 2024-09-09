using Library.Domain.Entities;
using Library.Domain.Requests;
using Library.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces.Services
{
	public interface ITokenService
	{
		Task<Tuple<string, string>?> GenerateTokensAsync(int userId);
		Task<Result<int>> ValidateRefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
		Task<bool> RemoveRefreshTokenAsync(User user);
	}
}
