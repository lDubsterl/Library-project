using AutoMapper;
using Library.Application.Interfaces.Repositories;
using Library.Domain.BaseEntities;
using Library.Domain.Entities;
using Library.Shared.Results;
using MediatR;

namespace Library.Application.Features.Books.Commands
{
    internal class DeleteBookCommand : BaseBook, IRequest<Result<int>> { }
	internal class DeleteBookHandler : IRequestHandler<DeleteBookCommand, Result<int>>
	{
		private IUnitOfWork _unitOfWork;
		private IMapper _mapper;
		public DeleteBookHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Result<int>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
		{
			var book = _mapper.Map<Book>(request);
			await _unitOfWork.Repository<Book>().DeleteAsync(book);
			return await Result<int>.SuccessAsync($"{book.Title} was deleted successfully");
		}
	}
}
