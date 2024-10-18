using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Features.User.Commands
{
    public class ExtendBookUsage : IRequest<IActionResult>
    {
        public int BookId { get; set; }
    }

    public class ExtendBookUsageHandler : IRequestHandler<ExtendBookUsage, IActionResult>
    {
        private IUnitOfWork _unitOfWork;

		public ExtendBookUsageHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IActionResult> Handle(ExtendBookUsage request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Repository<UserBook>().GetByIdAsync(request.BookId);
            book.RefundDate = book.RefundDate.AddDays(30);

            await _unitOfWork.Repository<UserBook>().UpdateAsync(book);
            await _unitOfWork.Save();
            return new OkObjectResult(new { Message = $"Usage extended successfully\nNew refund date: {book.RefundDate}" });
        }
    }
}
