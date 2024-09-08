using AutoMapper;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Features.Books.Queries
{
	internal class GetAllBooksQuery : IRequest<Result<List<Book>>> { }
	internal class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, Result<List<Book>>>
	{
		IUnitOfWork _unitOfWork;
		IMapper _mapper;

		public GetAllBooksQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Result<List<Book>>> Handle(GetAllBooksQuery query, CancellationToken cancellationToken)
		{
			var books = _unitOfWork.Repository<Book>().Entities.ToList();
			return await Result<List<Book>>.SuccessAsync(books);
		}
	}
}
