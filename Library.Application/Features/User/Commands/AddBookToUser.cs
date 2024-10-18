using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Application.DTOs;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Common;
using Library.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Features.User.Commands
{
    public class AddBookToUser : IRequest<IActionResult>, IMapTo<UserBook>
    {
        public string BookISBN { get; set; }

        public int UserId { get; set; }
        public int AuthorId { get; set; }
    }

    public class AddBookToUserHandler : IRequestHandler<AddBookToUser, IActionResult>
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public AddBookToUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(AddBookToUser request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Repository<Book>().Entities.FirstOrDefaultAsync(b => b.ISBN == request.BookISBN);
            book.IsOccupied = true;
            await _unitOfWork.Repository<Book>().UpdateAsync(book);

            var userBook = _mapper.Map<UserBook>(request);

            userBook.BookId = book.Id;
            userBook.IssueDate = DateOnly.FromDateTime(DateTime.Now);
            userBook.RefundDate = userBook.IssueDate.AddDays(30);

            _unitOfWork.Repository<UserBook>().Add(userBook);
            await _unitOfWork.Save();

            return new OkObjectResult(new { Message = $"Book was taken succesfully\nRefund date: " + userBook.RefundDate.ToString() });
        }
    }
}
