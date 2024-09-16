using AutoMapper;
using Library.Application.DTOs;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Library.Shared.Results;
using MediatR;

namespace Library.Application.Features.Authors.Queries
{
    public class GetAuthorById : IRequest<Result<AuthorDTO>>
	{
		public GetAuthorById(int id)
		{
			Id = id;
		}
		public int Id { get; set; }
	}

	public class GetAuthorByIdHandler : IRequestHandler<GetAuthorById, Result<AuthorDTO>>
	{
		IUnitOfWork _unitOfWork;
		IMapper _mapper;

		public GetAuthorByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Result<AuthorDTO>> Handle(GetAuthorById query, CancellationToken cancellationToken)
		{
			var author = await _unitOfWork.Repository<Author>().GetByIdAsync(query.Id);
			var mappedAuthor = _mapper.Map<AuthorDTO>(author);
			return await Result<AuthorDTO>.SuccessAsync(mappedAuthor);
		}
	}
}
