using Library.Shared.Results;
using MediatR;

namespace Library.Domain.Common
{
	public class SignUp: IRequest<Result<string>>
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
		public DateTime Ts {  get; set; }
		public string Role { get; set; }
	}
}
