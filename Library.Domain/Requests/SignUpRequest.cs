using Library.Shared.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Requests
{
	public class SignUpRequest: IRequest<Result<string>>
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
		public DateTime Ts {  get; set; }
		public string Role { get; set; }
	}
}
