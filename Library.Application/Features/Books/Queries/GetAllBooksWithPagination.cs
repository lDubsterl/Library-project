using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Application.Common.BaseClasses;
using Library.Application.DTOs;
using Library.Application.Extensions;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Library.Shared.Results;
using MediatR;

namespace Library.Application.Features.Books.Queries
{
    public class GetAllBooksWithPagination : PaginationBase, IRequest<PaginatedResult<BookDTO>>
	{
	}
	public class GetAllBooksHandler : IRequestHandler<GetAllBooksWithPagination, PaginatedResult<BookDTO>>
	{
		readonly IUnitOfWork _unitOfWork;
		readonly IMapper _mapper;

		public GetAllBooksHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<PaginatedResult<BookDTO>> Handle(GetAllBooksWithPagination query, CancellationToken cancellationToken)
		{
			return await _unitOfWork.Repository<Book>().Entities
				.ProjectTo<BookDTO>(_mapper.ConfigurationProvider)
				.ToPaginatedListAsync(query.PageNumber, query.PageSize);
		}
	}
}
