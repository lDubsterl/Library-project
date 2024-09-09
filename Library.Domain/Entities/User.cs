using Library.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
	public class User: IEntity
	{
		public int Id { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string PasswordSalt { get; set; }
		public DateTime Ts {  get; set; }
		public List<RefreshToken> RefreshTokens { get; set; }
	}
}
