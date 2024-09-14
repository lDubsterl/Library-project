using AutoMapper;
using Library.Application.DTOs;
using Library.Application.Extensions;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Library.Shared.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Features.Authors.Queries
{
	public class GetBooksByAuthorWithPagination(int pageNumber, int pageSize, int authorId) : PaginationBase(pageNumber, pageSize), IRequest<PaginatedResult<Book>>
	{
		public int AuthorId { get; set; } = authorId;
	}
	public class GetBooksByAuthorWithPaginationHandler : IRequestHandler<GetBooksByAuthorWithPagination, PaginatedResult<Book>>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetBooksByAuthorWithPaginationHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<PaginatedResult<Book>> Handle(GetBooksByAuthorWithPagination request, CancellationToken cancellationToken)
		{
			var author = await _unitOfWork.Repository<Author>().Entities
				.Where(a => a.Id == request.AuthorId)
				.SingleAsync();
			return author.Books.ToPaginatedList(request.PageNumber, request.PageSize);
		}
	}
}
