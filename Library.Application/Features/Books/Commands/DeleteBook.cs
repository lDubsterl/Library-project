using AutoMapper;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Features.Books.Commands
{
	public class DeleteBookCommand : IRequest<IActionResult>
	{
		public string ISBN { get; set; }
	}
	public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, IActionResult>
	{
		private IUnitOfWork _unitOfWork;
		public DeleteBookHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IActionResult> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
		{
			var bookFromDb = await _unitOfWork.Repository<Book>().Entities.FirstOrDefaultAsync(b => request.ISBN == b.ISBN);
			if (bookFromDb is null)
				return new BadRequestObjectResult(new { Message = "Book doesn't exist" });

			await _unitOfWork.Repository<Book>().DeleteAsync(bookFromDb);
			await _unitOfWork.Save();

			return new OkObjectResult(new { data = request.ISBN, Message = $"{bookFromDb.Title} was deleted successfully" });
		}
	}
}
