using AutoMapper;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using MediatR;
using Library.Shared;

namespace Library.Application.Features.Books.Commands
{
	internal class AddBookCommand : Book, IRequest<Result<int>> { }
	internal class AddBookHandler : IRequestHandler<AddBookCommand, Result<int>>
	{
		private IUnitOfWork _unitOfWork;
		private IMapper _mapper;
		public AddBookHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Result<int>> Handle(AddBookCommand request, CancellationToken cancellationToken)
		{
			var book = _mapper.Map<Book>(request);
			_unitOfWork.Repository<Book>().Add(book);
			await _unitOfWork.Save();
			return Result<int>.Success($"{book.Title} was added successfully");
		}
	}
}
