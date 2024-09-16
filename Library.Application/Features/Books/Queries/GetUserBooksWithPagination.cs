using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Application.Common.BaseClasses;
using Library.Application.DTOs;
using Library.Application.Extensions;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Library.Shared.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Features.Books.Queries
{
    public class GetUserBooksWithPagination : PaginationBase, IRequest<PaginatedResult<UserBookDTO>>
	{
		public int Id {  get; set; }
	}
	public class GetUserBooksHandler : IRequestHandler<GetUserBooksWithPagination, PaginatedResult<UserBookDTO>>
	{
		readonly IUnitOfWork _unitOfWork;
		readonly IMapper _mapper;

		public GetUserBooksHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<PaginatedResult<UserBookDTO>> Handle(GetUserBooksWithPagination request, CancellationToken cancellationToken)
		{
			return await _unitOfWork.Repository<UserBook>().Entities
				.Include(b => b.Book)
				.ThenInclude(a => a.Author)
				.Where(b => b.UserId == request.Id)
				.ProjectTo<UserBookDTO>(_mapper.ConfigurationProvider)
				.ToPaginatedListAsync(request.PageNumber, request.PageSize);
		}
	}
}
