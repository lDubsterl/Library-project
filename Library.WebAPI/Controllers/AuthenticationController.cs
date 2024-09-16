using Library.Application.DTOs;
using Library.Application.Features.Authentication;
using Library.Domain.Common;
using Library.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AuthenticationController : ApiBaseController
	{
		IMediator _mediator;

		public AuthenticationController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<Result<Tokens>> LogIn(Login request)
		{
			return await _mediator.Send(request);
		}

		[HttpPost]
		public async Task<Result<string>> SignUp(SignUp request)
		{
			return await _mediator.Send(request);
		}

		[HttpPost]
		public async Task<Result<string>> GetNewAccessToken(AccessToken request)
		{
			return await _mediator.Send(request);
		}

		[HttpGet]
		public async Task<Result<bool>> LogOut()
		{
			return await _mediator.Send(new LogOutRequest(UserId));
		}
	}
}
