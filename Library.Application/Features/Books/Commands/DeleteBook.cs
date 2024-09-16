using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Common;
using Library.Domain.Entities;
using Library.Shared.Results;
using MediatR;

namespace Library.Application.Features.Books.Commands
{
	public class DeleteBookCommand : IRequest<Result<int>>, IMapTo<Book>
	{
		public int Id { get; set; }
	}
	public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, Result<int>>
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
			var bookFromDb = await _unitOfWork.Repository<Book>().GetByIdAsync(request.Id);
			if (bookFromDb is null)
				return await Result<int>.FailureAsync("Book doesn't exist");

			await _unitOfWork.Repository<Book>().DeleteAsync(bookFromDb);
			await _unitOfWork.Save();
			return await Result<int>.SuccessAsync(request.Id, $"{bookFromDb.Title} was deleted successfully");
		}
	}
}
