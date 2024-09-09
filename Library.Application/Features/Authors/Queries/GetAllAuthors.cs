using AutoMapper;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Shared;
using MediatR;

namespace Library.Application.Features.Authors.Queries
{
	internal class GetAllAuthorsQuery : IRequest<Result<List<Author>>> { }
	internal class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, Result<List<Author>>>
	{
		IUnitOfWork _unitOfWork;
		IMapper _mapper;

		public GetAllAuthorsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Result<List<Author>>> Handle(GetAllAuthorsQuery query, CancellationToken cancellationToken)
		{
			var authors = _unitOfWork.Repository<Author>().Entities.ToList();
			return await Result<List<Author>>.SuccessAsync(authors);
		}
	}
}
