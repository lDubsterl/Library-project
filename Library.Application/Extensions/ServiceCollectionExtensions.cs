using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using MediatR;
using Library.Domain.Common;
using Library.Application.Features.Authors.Commands;
using Library.Application.Common.Validators;
using Library.Shared.Results;

namespace Library.Application.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static void AddApplicationLayer(this IServiceCollection services)
		{
			services.AddAutoMapper()
				.AddMediator()
				.AddValidators();
		}

		private static IServiceCollection AddAutoMapper(this IServiceCollection services)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			return services;
		}

		private static IServiceCollection AddMediator(this IServiceCollection services)
		{
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
			return services;
		}
		public static IServiceCollection AddValidators(this IServiceCollection services)
		{
			services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
			//services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			SetValidators(services);
			return services;
		}

		private static void SetValidators(IServiceCollection services)
		{
			var validationBases = new Type[] { typeof(BaseBook), typeof(BaseAuthor) };
			var interfaceName = nameof(IToValidate);

			bool ShouldBeValidated(Type t)
			{
				foreach (var validationBase in validationBases)
					if (t.GetInterface(interfaceName) != null)
						return true;
				return false;
			}

			var allTypes = Assembly.GetExecutingAssembly().GetExportedTypes();
			var typesToValidate = allTypes.Where(ShouldBeValidated).ToList();
			var validators = allTypes.Where(v => v.BaseType?.Name == typeof(AbstractValidator<>).Name).ToList();

			foreach (var type in typesToValidate)
			{
				var serviceType = typeof(IValidator<>).MakeGenericType(type);
				var abstractValidatorType = typeof(AbstractValidator<>).MakeGenericType(type.BaseType);
				var validatorType = validators.FirstOrDefault(v => v.BaseType == abstractValidatorType);
				services.AddScoped(serviceType, validatorType);
			}
		}
	}
}
