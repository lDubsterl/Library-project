using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Application.Extensions;
using Library.Application.DTOs;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using MediatR;
using Library.Application.Common.BaseClasses;
using Microsoft.AspNetCore.Mvc;

namespace Library.Application.Features.Authors.Queries
{
    public class GetAllAuthorsWithPagination : PaginationBase, IRequest<IActionResult>
	{
	}

	public class GetAllAuthorsWithPaginationHandler : IRequestHandler<GetAllAuthorsWithPagination, IActionResult>
	{
		IUnitOfWork _unitOfWork;
		IMapper _mapper;

		public GetAllAuthorsWithPaginationHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<IActionResult> Handle(GetAllAuthorsWithPagination request, CancellationToken cancellationToken)
		{
			return await _unitOfWork.Repository<Author>().Entities
				.ProjectTo<AuthorDTO>(_mapper.ConfigurationProvider)
				.ToPaginatedListAsync(request.PageNumber, request.PageSize);
		}
	}
}
