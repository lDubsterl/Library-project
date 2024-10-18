using Library.Application.Features.User.Commands;
using Library.Application.Features.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Library.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
	[ApiController]
	public class UserController : ApiBaseController
	{
		IMediator _mediator;
		public UserController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet, Authorize]
		public async Task<IActionResult> GetUserBooks([FromQuery] GetUserBooksWithPagination query)
		{
			query.Id = UserId;
			return await _mediator.Send(query);
		}

		[HttpPost, Authorize]
		public async Task<IActionResult> TakeBook(AddBookToUser request)
		{
			request.UserId = UserId;
			return await _mediator.Send(request);
		}

		[HttpPatch, Authorize]
		public async Task<IActionResult> ExtendBookUsage(ExtendBookUsage query)
		{
			return await _mediator.Send(query);
		}

		[HttpDelete, Authorize]
		public async Task<IActionResult> ReturnBook(int bookId)
		{
			return await _mediator.Send(new ReturnBook(bookId));
		}
	}
}
