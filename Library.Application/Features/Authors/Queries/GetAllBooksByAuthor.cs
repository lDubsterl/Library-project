using AutoMapper;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Library.Shared.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
		IUnitOfWork _unitOfWork;

		public GetAllBooksByAuthorHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<Result<List<Book>?>> Handle(GetAllBooksByAuthor query, CancellationToken cancellationToken)
		{
			var author = await _unitOfWork.Repository<Author>().Entities
				.Where(a => a.Id == query.Id)
				.FirstOrDefaultAsync();
			return await Result<List<Book>?>.SuccessAsync(author?.Books.ToList(), "Books were got successfully");
		}
	}
}
