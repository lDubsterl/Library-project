using AutoMapper;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Library.Shared.Results;
using MediatR;

namespace Library.Application.Features.Authors.Queries
{
    internal class GetAuthorByIdQuery : IRequest<Result<Author>>
	{
		public GetAuthorByIdQuery(int id)
		{
			Id = id;
		}
		public int Id { get; set; }
	}

	internal class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, Result<Author>>
	{
		IUnitOfWork _unitOfWork;
		IMapper _mapper;

		public GetAuthorByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Result<Author>> Handle(GetAuthorByIdQuery query, CancellationToken cancellationToken)
		{
			var author = await _unitOfWork.Repository<Author>().GetByIdAsync(query.Id);
			return await Result<Author>.SuccessAsync(author!);
		}
	}
}
