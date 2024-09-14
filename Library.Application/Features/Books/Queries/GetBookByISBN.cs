using AutoMapper;
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
    internal class GetBookByISBN(string isbn) : IRequest<Result<Book>>
	{
		public string ISBN { get; set; } = isbn;
	}

	internal class GetBookByISBNHandler : IRequestHandler<GetBookByISBN, Result<Book>>
	{
		IUnitOfWork _unitOfWork;

		public GetBookByISBNHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<Result<Book>> Handle(GetBookByISBN query, CancellationToken cancellationToken)
		{
			var book = await _unitOfWork.Repository<Book>().Entities.Where(b => b.ISBN == query.ISBN).FirstOrDefaultAsync();
			return await Result<Book>.SuccessAsync(book!);
		}
	}
}
