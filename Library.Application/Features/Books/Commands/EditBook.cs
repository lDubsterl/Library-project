using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Application.DTOs;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Common;
using Library.Domain.Entities;
using Library.Shared.Results;
using MediatR;

namespace Library.Application.Features.Books.Commands
{
	public class EditBookCommand : BaseBook, IRequest<Result<int>>, IMapTo<Book>
	{
		public int Id { get; set; }
		public int AuthorId { get; set; }
		public AuthorDTO Author { get; set; }
	}
	public class EditBookHandler : IRequestHandler<EditBookCommand, Result<int>>
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
			var bookFromDb = await _unitOfWork.Repository<Book>().GetByIdAsync(request.Id);

			if (bookFromDb is null)
				return await Result<int>.FailureAsync("Book not found");

			await _unitOfWork.Repository<Book>().UpdateAsync(book);
			await _unitOfWork.Save();
			return await Result<int>.SuccessAsync($"{book.Title} was updated successfully");
		}
	}
}
