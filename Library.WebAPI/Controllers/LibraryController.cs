using Library.Application.DTOs;
using Library.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
		public async Task<Result<List<BookDTO>>> GetUserBooks(int userId)
		{
			return await _mediator.Send(userId);
		}
	}
}
