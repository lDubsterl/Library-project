using Library.Application.Features.Authors.Commands;
using Library.Application.Features.Authors.Queries;
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
		public async Task<IActionResult> GetAllAuthors([FromQuery] GetAllAuthorsWithPagination query)
		{
			return await _mediator.Send(query);
		}

		[HttpGet]
		public async Task<IActionResult> GetAuthorById(int Id)
		{
			return await _mediator.Send(new GetAuthorById(Id));
		}

		[HttpGet]
		public async Task<IActionResult> GetAllBooksByAuthor([FromQuery] GetBooksByAuthorWithPagination query)
		{
			return await _mediator.Send(query);
		}

		[HttpPost, Authorize(Policy = "AdminsOnly")]
		public async Task<IActionResult> AddAuthor(AddAuthorCommand query)
		{
			return await _mediator.Send(query);
		}

		[HttpPut, Authorize(Policy = "AdminsOnly")]
		public async Task<IActionResult> EditAuthor(EditAuthorCommand query)
		{
			return await _mediator.Send(query);
		}

		[HttpDelete, Authorize(Policy = "AdminsOnly")]
		public async Task<IActionResult> DeleteAuthor([FromQuery]DeleteAuthorCommand query)
		{
			return await _mediator.Send(query);
		}
	}
}
