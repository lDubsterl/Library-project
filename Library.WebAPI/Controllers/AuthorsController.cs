using Library.Application.DTOs;
using Library.Application.Features.Authors.Commands;
using Library.Application.Features.Authors.Queries;
using Library.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
		public async Task<ActionResult<PaginatedResult<AuthorDTO>>> GetAllAuthors([FromQuery] GetAllAuthorsWithPagination query)
		{
			return await _mediator.Send(query);
		}

		[HttpGet]
		public async Task<Result<AuthorDTO>> GetAuthorById(int authorId)
		{
			return await _mediator.Send(new GetAuthorById(authorId));
		}

		[HttpGet]
		public async Task<ActionResult<Result<BookDTO>>> GetAllBooksByAuthor([FromQuery] GetBooksByAuthorWithPagination query)
		{
			return await _mediator.Send(query);
		}

		[HttpPost, Authorize(Policy = "AdminsOnly")]
		public async Task<ActionResult<Result<int>>> AddAuthor(AddAuthorCommand query)
		{
			return await _mediator.Send(query);
		}

		[HttpPut, Authorize(Policy = "AdminsOnly")]
		public async Task<ActionResult<Result<int>>> EditAuthor(EditAuthorCommand query)
		{
			return await _mediator.Send(query);
		}

		[HttpDelete, Authorize(Policy = "AdminsOnly")]
		public async Task<Result<int>> DeleteAuthor(DeleteAuthorCommand query)
		{
			return await _mediator.Send(query);
		}
	}
}
