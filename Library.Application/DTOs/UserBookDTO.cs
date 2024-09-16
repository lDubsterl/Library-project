using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Domain.Common;
using Library.Domain.Entities;
using MediatR;

namespace Library.Application.DTOs
{
	public class UserBookDTO: BaseBook, IMapFrom<UserBook>
	{
		public DateTime IssueDate { get; set; }
		public DateTime RefundDate { get; set; }
	}
}
