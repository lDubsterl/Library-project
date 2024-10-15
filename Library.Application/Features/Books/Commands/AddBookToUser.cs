using Library.Application.Common.Mappings;
using Library.Application.DTOs;
using Library.Domain.Common;
using Library.Domain.Entities;
using Library.Shared.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Features.Books.Commands
{
	public class AddBookToUser: BaseBook, IRequest<Result<int>>, IMapTo<Book>, IMapTo<BookDTO>
	{
		public AuthorDTO Author { get; set; }
	}
}
