using Library.Application.DTOs;
using Library.Application.Features.Books.Queries;
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
		public async Task<IActionResult> GetAllBooks([FromQuery] GetAllBooksWithPagination query)
		{
			return await _mediator.Send(query);
		}

		[HttpGet]
		public async Task<IActionResult> GetBookById([FromQuery] GetBookById query)
		{
			return await _mediator.Send(query);
		}

		[HttpGet]
		public async Task<IActionResult> GetBookByISBN([FromQuery] GetBookByISBN query)
		{
			return await _mediator.Send(query);
		}

		[HttpGet, Authorize]
		public async Task<IActionResult> GetUserBooks([FromQuery] GetUserBooksWithPagination query)
		{
			return await _mediator.Send(query);
		}

		[HttpPost, Authorize(Policy = "AdminsOnly")]
		public async Task<IActionResult> AddBook(AddBookCommand query)
		{
			return await _mediator.Send(query);
		}

		[HttpPost, Authorize(Policy = "AdminsOnly")]
		public async Task<IActionResult> AddImageToBook(AddImageCommand query)
		{
			return await _mediator.Send(query);
		}

		[HttpPut, Authorize(Policy = "AdminsOnly")]
		public async Task<IActionResult> EditBook(EditBookCommand query)
		{
			return await _mediator.Send(query);
		}

		[HttpDelete, Authorize(Policy = "AdminsOnly")]
		public async Task<IActionResult> DeleteBook([FromQuery]DeleteBookCommand query)
		{
			return await _mediator.Send(query);
		}

		[HttpPost, Authorize]
		public async Task<IActionResult> TakeBook(AddBookToUser query)
		{
			return await _mediator.Send(query);
		}
	}
}
