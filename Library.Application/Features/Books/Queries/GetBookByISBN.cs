using AutoMapper;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Library.Shared.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Features.Books.Queries
{
    internal class GetBookByISBN : IRequest<Result<Book>>
	{
		public GetBookByISBN(string isbn)
		{
			ISBN = isbn;
		}
		public string ISBN { get; set; }
	}

	internal class GetBookByISBNHandler : IRequestHandler<GetBookByISBN, Result<Book>>
	{
		IBooksRepository _repository;

		public GetBookByISBNHandler(IBooksRepository repo)
		{
			_repository = repo;
		}

		public async Task<Result<Book>> Handle(GetBookByISBN query, CancellationToken cancellationToken)
		{
			var book = await _repository.GetBookByISBN(query.ISBN);
			return await Result<Book>.SuccessAsync(book!);
		}
	}
}
