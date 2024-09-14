using AutoMapper;
using Library.Domain.Entities;
using MediatR;
using Library.Application.Interfaces.Repositories;
using Library.Shared.Results;
using Library.Domain.BaseEntities;

namespace Library.Application.Features.Books.Commands
{
    internal class AddBookCommand : BaseBook, IRequest<Result<int>> { }
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
			return await Result<int>.SuccessAsync($"{book.Title} was added successfully");
		}
	}
}
