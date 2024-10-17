using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Application.Common.Validators;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Common;
using Library.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Features.Books.Commands
{
	public class EditBookCommand : BaseBook, IRequest<IActionResult>, IMapTo<Book>, IToValidate
	{
		public int AuthorId { get; set; }
	}
	public class EditBookHandler : IRequestHandler<EditBookCommand, IActionResult>
	{
		private IUnitOfWork _unitOfWork;
		public EditBookHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IActionResult> Handle(EditBookCommand request, CancellationToken cancellationToken)
		{
			var bookFromDb = await _unitOfWork.Repository<Book>().Entities.FirstOrDefaultAsync(b => b.ISBN == request.ISBN);

			if (bookFromDb is null)
				return new BadRequestObjectResult(new { Message = "Book not found" });

			bookFromDb.Title = request.Title;
			bookFromDb.Genre = request.Genre;
			bookFromDb.Description = request.Description;
			bookFromDb.Author = await _unitOfWork.Repository<Author>().GetByIdAsync(request.AuthorId);

			await _unitOfWork.Repository<Book>().UpdateAsync(bookFromDb);
			await _unitOfWork.Save();

			return new OkObjectResult(new { Message = $"{request.Title} was updated successfully" });
		}
	}
}
