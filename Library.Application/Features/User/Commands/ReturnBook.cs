using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Features.User.Commands
{
	public class ReturnBook: IRequest<IActionResult>
	{
		public int BookId { get; set; }

		public ReturnBook(int bookId)
		{
			BookId = bookId;
		}
	}

	public class ReturnBookHandler : IRequestHandler<ReturnBook, IActionResult>
	{
		private IUnitOfWork _unitOfWork;

		public ReturnBookHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IActionResult> Handle(ReturnBook request, CancellationToken cancellationToken)
		{
			var book = await _unitOfWork.Repository<Book>().GetByIdAsync(request.BookId);
			var userBook = await _unitOfWork.Repository<UserBook>().Entities.FirstOrDefaultAsync(ub => ub.BookId == request.BookId);

			book.IsOccupied = false;

			await _unitOfWork.Repository<UserBook>().DeleteAsync(userBook);
			await _unitOfWork.Repository<Book>().UpdateAsync(book);
			await _unitOfWork.Save();

			return new OkObjectResult(new { message = "Book was returned successfully" });
		}
	}
}
