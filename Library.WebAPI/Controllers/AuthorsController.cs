using Library.Application.DTOs;
using Library.Application.Features.Authors.Commands;
using Library.Application.Features.Authors.Queries;
using Library.Application.Features.Books.Commands;
using Library.Application.Features.Validators;
using Library.Application.Validators;
using Library.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GV = Library.Application.Features.Validators.GenericValidator;

namespace Library.WebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AuthorsController : ApiBaseController
	{
		IMediator _mediator;

		public AuthorsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<ActionResult<PaginatedResult<AuthorDTO>>> GetAllAuthors([FromQuery]GetAllAuthorsWithPagination query)
		{
			var result = GV.ValidateQuery(typeof(PaginationValidator), query);
			if (result is null)
				return await _mediator.Send(query);
			else
				return BadRequest(result);
		}

		[HttpGet]
		public async Task<Result<AuthorDTO>> GetAuthorById(int authorId)
		{
			return await _mediator.Send(new GetAuthorById(authorId));
		}

		[HttpGet]
		public async Task<ActionResult<Result<BookDTO>>> GetAllBooksByAuthor([FromQuery]GetBooksByAuthorWithPagination query)
		{
			var result = GV.ValidateQuery(typeof(PaginationValidator), query);
			if (result is null)
				return await _mediator.Send(query);
			else
				return BadRequest(result);
		}

		[HttpPost, Authorize(Policy = "AdminsOnly")]
		public async Task<ActionResult<Result<int>>> AddAuthor(AddAuthorCommand query)
		{
			var result = GV.ValidateQuery(typeof(AuthorValidator), query);
			if (result is null)
				return await _mediator.Send(query);
			else
				return BadRequest(result);
		}

		[HttpPut, Authorize(Policy = "AdminsOnly")]
		public async Task<ActionResult<Result<int>>> EditAuthor(EditAuthorCommand query)
		{
			var result = GV.ValidateQuery(typeof(AuthorValidator), query);
			if (result is null)
				return await _mediator.Send(query);
			else
				return BadRequest(result);
		}

		[HttpDelete, Authorize(Policy = "AdminsOnly")]
		public async Task<Result<int>> DeleteAuthor(DeleteAuthorCommand query)
		{
			return await _mediator.Send(query);
		}
	}
}
