using AutoMapper;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Library.Shared.Results;
using MediatR;

namespace Library.Application.Features.Books.Commands
{
    internal class EditBookCommand : Book, IRequest<Result<int>> { }
	internal class EditBookHandler : IRequestHandler<EditBookCommand, Result<int>>
	{
		private IUnitOfWork _unitOfWork;
		private IMapper _mapper;
		public EditBookHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Result<int>> Handle(EditBookCommand request, CancellationToken cancellationToken)
		{
			var book = _mapper.Map<Book>(request);
			await _unitOfWork.Repository<Book>().UpdateAsync(book);
			return await Result<int>.SuccessAsync($"{book.Title} was updated successfully");
		}
	}
}
