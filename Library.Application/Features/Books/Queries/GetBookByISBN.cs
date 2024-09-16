using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Application.DTOs;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Library.Shared.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Features.Books.Queries
{
	public class GetBookByISBN : IRequest<Result<BookDTO>>
	{
		public string ISBN { get; set; }
	}

	public class GetBookByISBNHandler : IRequestHandler<GetBookByISBN, Result<BookDTO>>
	{
		IUnitOfWork _unitOfWork;
		IMapper _mapper;
		public GetBookByISBNHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Result<BookDTO>> Handle(GetBookByISBN query, CancellationToken cancellationToken)
		{
			var book = await _unitOfWork.Repository<Book>().Entities
				.Where(b => b.ISBN == query.ISBN)
				.ProjectTo<BookDTO>(_mapper.ConfigurationProvider)
				.SingleAsync();
			return await Result<BookDTO>.SuccessAsync(book!);
		}
	}
}
