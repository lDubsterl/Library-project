using Library.Shared.Results;
using MediatR;

namespace Library.Domain.Common
{
	public class Login: IRequest<Result<Tokens>>
	{
		public string Email {  get; set; }
		public string Password { get; set; }
	}
}
