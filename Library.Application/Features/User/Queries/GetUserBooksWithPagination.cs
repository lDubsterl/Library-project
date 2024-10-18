using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Application.Common.BaseClasses;
using Library.Application.DTOs;
using Library.Application.Extensions;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Features.User.Queries
{
    public class GetUserBooksWithPagination : PaginationBase, IRequest<IActionResult>
    {
        public int Id;

	}
    public class GetUserBooksHandler : IRequestHandler<GetUserBooksWithPagination, IActionResult>
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;

        public GetUserBooksHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(GetUserBooksWithPagination request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Repository<UserBook>().Entities
                .Where(b => b.UserId == request.Id)
                .Include(b => b.Book)
                .ThenInclude(a => a.Author)
                .ProjectTo<UserBookDTO>(_mapper.ConfigurationProvider)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
