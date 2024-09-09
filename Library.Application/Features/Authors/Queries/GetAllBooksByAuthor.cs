using AutoMapper;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Library.Shared.Results;
using MediatR;

namespace Library.Application.Features.Authors.Queries
{
    internal class GetAllBooksByAuthor: IRequest<Result<List<Book>?>>
	{
		public GetAllBooksByAuthor(int authorId)
		{
			Id = authorId;
		}
		public int Id { get; set; }
	}
	internal class GetAllBooksByAuthorHandler: IRequestHandler<GetAllBooksByAuthor, Result<List<Book>?>>
	{
		IAuthorsRepository _repository;
		IMapper _mapper;

		public GetAllBooksByAuthorHandler(IAuthorsRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<Result<List<Book>?>> Handle(GetAllBooksByAuthor query, CancellationToken cancellationToken)
		{
			var books = await _repository.GetBooksByAuthor(query.Id);
			return await Result<List<Book>?>.SuccessAsync(books);
		}
	}
}
