using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Domain.Common;
using Library.Domain.Entities;
using MediatR;

namespace Library.Application.DTOs
{
	public class UserBookDTO:  IMapFrom<UserBook>
	{
		public BookDTO Book { get; set; }
		public DateOnly IssueDate { get; set; }
		public DateOnly RefundDate { get; set; }
	}
}
