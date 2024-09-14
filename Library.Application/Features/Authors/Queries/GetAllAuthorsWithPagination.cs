using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Application.Extensions;
using Library.Application.DTOs;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Library.Shared.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Features.Authors.Queries
{
	public class GetAllAuthorsWithPagination(int pageSize, int pageNumber) : PaginationBase(pageSize, pageNumber), IRequest<PaginatedResult<AuthorDTO>>
	{
	}

	public class GetAllAuthorsWithPaginationHandler : IRequestHandler<GetAllAuthorsWithPagination, PaginatedResult<AuthorDTO>>
	{
		IUnitOfWork _unitOfWork;
		IMapper _mapper;

		public GetAllAuthorsWithPaginationHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<PaginatedResult<AuthorDTO>> Handle(GetAllAuthorsWithPagination request, CancellationToken cancellationToken)
		{
			return await _unitOfWork.Repository<Author>().Entities
				.ProjectTo<AuthorDTO>(_mapper.ConfigurationProvider)
				.ToPaginatedListAsync(request.PageNumber, request.PageSize);
		}
	}
}
