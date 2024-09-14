using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.DTOs
{
	internal class AuthorDTO: IRequest
	{
		public required string Name { get; set; }
		public required string Surname { get; set; }
		public required DateOnly BirthDate { get; set; }
	}
}
