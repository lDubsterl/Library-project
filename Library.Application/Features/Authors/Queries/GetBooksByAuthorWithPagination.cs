using AutoMapper;
using Library.Application.Common.BaseClasses;
using Library.Application.DTOs;
using Library.Application.Extensions;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Features.Authors.Queries
{
	public class GetBooksByAuthorWithPagination : PaginationBase, IRequest<IActionResult>
	{
		public int AuthorId { get; set; }
	}
	public class GetBooksByAuthorWithPaginationHandler : IRequestHandler<GetBooksByAuthorWithPagination, IActionResult>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetBooksByAuthorWithPaginationHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<IActionResult> Handle(GetBooksByAuthorWithPagination request, CancellationToken cancellationToken)
		{
			var author = await _unitOfWork.Repository<Author>().Entities
				.Include(b => b.Books)
				.Where(a => a.Id == request.AuthorId)
				.SingleAsync();

			if (author is null)
				return new BadRequestObjectResult(new { Message = "Author doesn't exist" });

			var books = author.Books?
				.Select(_mapper.Map<BookDTO>)
				.ToPaginatedList(request.PageNumber, request.PageSize);

			return books;
		}
	}
}
