using AutoMapper;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Library.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
