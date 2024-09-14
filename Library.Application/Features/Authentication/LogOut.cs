using Library.Application.Interfaces.Services;
using Library.Shared.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Features.Authentication
{
	public record LogOutRequest : IRequest<Result<bool>>
	{
		public LogOutRequest(int id)
		{
			Id = id;
		}

		public int Id { get; set; }

	}
	public class LogOutHandler : IRequestHandler<LogOutRequest, Result<bool>>
	{
		IAuthenticationService _service;

		public LogOutHandler(IAuthenticationService service)
		{
			_service = service;
		}

		public async Task<Result<bool>> Handle(LogOutRequest request, CancellationToken cancellationToken)
		{
			return await _service.LogoutAsync(request.Id);
		}
	}
}
