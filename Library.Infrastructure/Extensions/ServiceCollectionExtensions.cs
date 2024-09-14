using Library.Application.Interfaces.Services;
using Library.Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static void AddInfrastructureLayer(this IServiceCollection services)
		{
			services.AddServices();
		}

		private static void AddServices(this IServiceCollection services)
		{
			services
				.AddTransient<IMediator, Mediator>()
				.AddTransient<IAuthenticationService, AuthenticationService>()
				.AddTransient<ITokenService, TokenService>();
		}
	}
}
