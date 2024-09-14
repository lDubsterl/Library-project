using Library.Domain.Entities;
using Library.Shared.Results;
using MediatR;

namespace Library.Domain.Requests
{
	public class RefreshTokenRequest: IRequest<Result<Tokens>>
	{
		public int UserId { get; set; }
		public string RefreshToken { get; set; }
	}
}
