using AutoMapper;
using Library.Domain.Entities;
using MediatR;
using Library.Application.Interfaces.Repositories;
using Library.Shared.Results;
using Library.Domain.Common;
using Library.Application.Common.Mappings;
using Library.Application.DTOs;
using Library.Application.Common.Validators;

namespace Library.Application.Features.Books.Commands
{
	public class AddBookCommand : BaseBook, IRequest<Result<int>>, IMapTo<Book>, IMapTo<BookDTO>, IToValidate
	{
		public AuthorDTO Author { get; set; }
	}
	public class AddBookHandler : IRequestHandler<AddBookCommand, Result<int>>
	{
		private IUnitOfWork _unitOfWork;
		private IMapper _mapper;
		public AddBookHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		bool IsEqual(Book fromDb, AddBookCommand fromRequest) => _mapper.Map<BookDTO>(fromDb).Equals(_mapper.Map<BookDTO>(fromRequest));
		public async Task<Result<int>> Handle(AddBookCommand request, CancellationToken cancellationToken)
		{
			var author = _unitOfWork.Repository<Author>().Entities
				.AsEnumerable()
				.Where(e => _mapper.Map<AuthorDTO>(e).Equals(request.Author))
				.SingleOrDefault();

			if (author == null)
				return await Result<int>.FailureAsync("Author not found");

			var isBookExists = _unitOfWork.Repository<Book>().Entities
				.AsEnumerable()
				.Any(a => IsEqual(a, request));

			if (isBookExists)
				return await Result<int>.FailureAsync("Book with this ISBN already exists");
			
			var book = _mapper.Map<Book>(request);
			book.Author = author;
			book = _unitOfWork.Repository<Book>().Add(book);

			//author.Books = author.Books.Append(book);
			//await _unitOfWork.Repository<Author>().UpdateAsync(author);

			await _unitOfWork.Save();
			return await Result<int>.SuccessAsync(book.Id, $"{book.Title} was added successfully");
		}
	}
}
