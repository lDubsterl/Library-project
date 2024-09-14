using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.DTOs
{
	public class BookDTO: IRequest
	{
		public required string ISBN { get; set; }
		public required string Title { get; set; }
		public required string Genre { get; set; }
		public required string Description { get; set; }
		public required string Author { get; set; }
	}
}
