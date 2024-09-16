using AutoMapper;
using Library.Application.DTOs;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Library.Shared.Results;
using MediatR;

namespace Library.Application.Features.Books.Queries
{
    public class GetBookById : IRequest<Result<BookDTO>>
	{
		public int Id { get; set; }
	}

	public class GetBookByIdHandler : IRequestHandler<GetBookById, Result<BookDTO>>
	{
		IUnitOfWork _unitOfWork;
		IMapper _mapper;

		public GetBookByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Result<BookDTO>> Handle(GetBookById query, CancellationToken cancellationToken)
		{
			var book = await _unitOfWork.Repository<Book>()
				.GetByIdAsync(query.Id);
			var mappedBook = _mapper.Map<BookDTO>(book);
			return await Result<BookDTO>.SuccessAsync(mappedBook!);
		}
	}
}
