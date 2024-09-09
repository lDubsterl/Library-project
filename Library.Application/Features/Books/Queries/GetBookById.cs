using AutoMapper;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Library.Shared.Results;
using MediatR;

namespace Library.Application.Features.Books.Queries
{
    internal class GetBookByIdQuery : IRequest<Result<Book>>
	{
		public GetBookByIdQuery(int id)
		{
			Id = id;
		}
		public int Id { get; set; }
	}

	internal class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Result<Book>>
	{
		IUnitOfWork _unitOfWork;
		IMapper _mapper;

		public GetBookByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Result<Book>> Handle(GetBookByIdQuery query, CancellationToken cancellationToken)
		{
			var book = await _unitOfWork.Repository<Book>().GetByIdAsync(query.Id);
			return await Result<Book>.SuccessAsync(book!);
		}
	}
}
