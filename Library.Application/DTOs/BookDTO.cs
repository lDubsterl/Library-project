using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Domain.BaseEntities;
using Library.Domain.Entities;

namespace Library.Application.DTOs
{
	public class BookDTO : BaseBook, IMapFromSpecific, IMapFrom<Book>
	{
		
		public required string Author { get; set; }

		public void SpecificMapping(Profile profile)
		{
			profile.CreateMap<Author, BookDTO>()
				.ForMember(bd => bd.Author, b => b.MapFrom(s => $"{s.Surname} {s.Name}"));
		}
	}
}
