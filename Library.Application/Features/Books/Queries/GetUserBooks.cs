using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Application.DTOs;
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
	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<Book, BookDTO>();
			CreateMap<Author, BookDTO>()
				.ForMember(bd => bd.Author, b => b.MapFrom(s => $"{s.Surname} {s.Name}"));
		}
	}
	public class GetUserBooks: IRequest<Result<List<BookDTO>>>
	{
			public GetUserBooks(int id)
			{
				Id = id;
			}

			public int Id { get; set; }

	}
	public class GetUserBooksHandler : IRequestHandler<GetUserBooks, Result<List<BookDTO>>>
	{
		IUnitOfWork _unitOfWork;
		IMapper _mapper;

		public GetUserBooksHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Result<List<BookDTO>>> Handle(GetUserBooks request, CancellationToken cancellationToken)
		{
			var bookList = await _unitOfWork.Repository<UserBook>().Entities
				.Include(b => b.Book)
				.ThenInclude(a => a.Author)
				.Where(b => b.UserId == request.Id)
				.ProjectTo<BookDTO>(_mapper.ConfigurationProvider).ToListAsync();
				return await Result<List<BookDTO>>.SuccessAsync(bookList, "");
		}
	}
}
