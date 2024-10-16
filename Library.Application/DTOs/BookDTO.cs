using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Application.Features.Books.Commands;
using Library.Domain.Common;
using Library.Domain.Entities;

namespace Library.Application.DTOs
{
	public class BookDTO : BaseBook, IMapFrom<Book>
	{
		public string PathToImage { get; set; }
		public AuthorDTO Author { get; set; }

	}
}
