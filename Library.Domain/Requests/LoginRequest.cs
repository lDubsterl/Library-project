using Library.Domain.Entities;
using Library.Shared.Results;
using MediatR;

namespace Library.Domain.Requests
{
	public class LoginRequest: IRequest<Result<Tokens>>
	{
		public string Email {  get; set; }
		public string Password { get; set; }
	}
}
