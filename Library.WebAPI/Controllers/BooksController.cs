using Library.Application.DTOs;
using Library.Application.Features.Books.Queries;
using Library.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Library.Application.Features.Books.Commands;

namespace Library.WebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class BooksController : ApiBaseController
	{
		IMediator _mediator;
		public BooksController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<ActionResult<PaginatedResult<BookDTO>>> GetAllBooks([FromQuery] GetAllBooksWithPagination query)
		{
			return await _mediator.Send(query);
		}

		[HttpGet]
		public async Task<Result<BookDTO>> GetBookById([FromQuery] GetBookById query)
		{
			return await _mediator.Send(query);
		}

		[HttpGet]
		public async Task<Result<BookDTO>> GetBookByISBN([FromQuery] GetBookByISBN query)
		{
			return await _mediator.Send(query);
		}

		[HttpGet, Authorize]
		public async Task<ActionResult<PaginatedResult<UserBookDTO>>> GetUserBooks([FromQuery] GetUserBooksWithPagination query)
		{
			return await _mediator.Send(query);
		}

		[HttpPost, Authorize(Policy = "AdminsOnly")]
		public async Task<ActionResult<Result<int>>> AddBook(AddBookCommand query)
		{
			return await _mediator.Send(query);
		}

		[HttpPut, Authorize(Policy = "AdminsOnly")]
		public async Task<ActionResult<Result<int>>> EditBook(EditBookCommand query)
		{
			return await _mediator.Send(query);
		}

		[HttpDelete, Authorize(Policy = "AdminsOnly")]
		public async Task<ActionResult<Result<int>>> DeleteBook(DeleteBookCommand query)
		{
			return await _mediator.Send(query);
		}
	}
}
