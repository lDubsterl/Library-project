using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Domain.Entities;
using MediatR;

namespace Library.Application.DTOs
{
	public class UserBookDTO: IMapFrom<UserBook>, IMapFromSpecific
	{
		public required string ISBN { get; set; }
		public required string Title { get; set; }
		public required string Genre { get; set; }
		public required string Description { get; set; }
		public required string Author { get; set; }
		public DateTime IssueDate { get; set; }
		public DateTime RefundDate { get; set; }
		public void SpecificMapping(Profile profile)
		{
			profile.CreateMap<Author, UserBookDTO>()
				.ForMember(bd => bd.Author, b => b.MapFrom(s => $"{s.Surname} {s.Name}"));
		}
	}
}
