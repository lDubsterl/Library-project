using Library.Application.DTOs;
using Library.Application.Features.Validators;
using Library.Application.Features.Books.Queries;
using Library.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GV = Library.Application.Features.Validators.GenericValidator;
using Library.Domain.Entities;
using Library.Application.Features.Books.Commands;
using Library.Application.Validators;

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
		public async Task<ActionResult<PaginatedResult<BookDTO>>> GetAllBooks([FromQuery]GetAllBooksWithPagination query)
		{
			var result = GV.ValidateQuery(typeof(PaginationValidator), query);
			if (result is null)
				return await _mediator.Send(query);
			else
				return BadRequest(result);
		}

		[HttpGet]
		public async Task<Result<BookDTO>> GetBookById([FromQuery]GetBookById query)
		{
			return await _mediator.Send(query);
		}

		[HttpGet]
		public async Task<Result<BookDTO>> GetBookByISBN([FromQuery]GetBookByISBN query)
		{
			return await _mediator.Send(query);
		}

		[HttpGet, Authorize]
		public async Task<ActionResult<PaginatedResult<UserBookDTO>>> GetUserBooks([FromQuery]GetUserBooksWithPagination query)
		{
			var result = GV.ValidateQuery(typeof(PaginationValidator), query);
			if (result is null)
				return await _mediator.Send(query);
			else
				return BadRequest(result);
		}

		[HttpPost, Authorize(Policy = "AdminsOnly")]
		public async Task<ActionResult<Result<int>>> AddBook(AddBookCommand query)
		{
			var result = GV.ValidateQuery(typeof(BookValidator), query);
			if (result is null)
				return await _mediator.Send(query);
			else
				return BadRequest(result);
		}

		[HttpPut, Authorize(Policy = "AdminsOnly")]
		public async Task<ActionResult<Result<int>>> EditBook(EditBookCommand query)
		{
			var result = GV.ValidateQuery(typeof(BookValidator), query);
			if (result is null)
				return await _mediator.Send(query);
			else
				return BadRequest(result);
		}

		[HttpDelete, Authorize(Policy = "AdminsOnly")]
		public async Task<ActionResult<Result<int>>> DeleteBook(DeleteBookCommand query)
		{
			return await _mediator.Send(query);
		}
	}
}
