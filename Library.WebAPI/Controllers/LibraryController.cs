using Library.Application.DTOs;
using Library.Application.Features.Validators;
using Library.Application.Features.Books.Queries;
using Library.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GV = Library.Application.Features.Validators.GenericValidator;

namespace Library.WebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class LibraryController : ApiBaseController
	{
		IMediator _mediator;
		public LibraryController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		[Authorize]
		public async Task<ActionResult<PaginatedResult<UserBookDTO>>> GetUserBooks(GetUserBooksWithPagination query)
		{
			var validator = new PaginationValidator();
			var result = validator.Validate(query);
			if (result.IsValid)
				return await _mediator.Send(query);
			else 
				return BadRequest(result.Errors.Select(e => e.ErrorMessage).ToList());
		}

		[HttpGet]
		[Authorize]
		public async Task<ActionResult<PaginatedResult<BookDTO>>> GetAllBooks(GetAllBooksWithPagination query)
		{
			var result = GV.ValidateQuery(typeof(PaginationValidator), query);
			if (result is null)
				return await _mediator.Send(query);
			else
				return BadRequest(result);
		}

	}
}
