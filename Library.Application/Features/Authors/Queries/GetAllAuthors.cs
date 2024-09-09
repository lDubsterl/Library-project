using AutoMapper;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Library.Shared.Results;
using MediatR;

namespace Library.Application.Features.Authors.Queries
{
    internal class GetAllAuthorsQuery : IRequest<Result<List<Author>>> { }
	internal class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, Result<List<Author>>>
	{
		IUnitOfWork _unitOfWork;

		public GetAllAuthorsQueryHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<Result<List<Author>>> Handle(GetAllAuthorsQuery query, CancellationToken cancellationToken)
		{
			var authors = _unitOfWork.Repository<Author>().Entities.ToList();
			return await Result<List<Author>>.SuccessAsync(authors);
		}
	}
}
