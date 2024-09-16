using FluentValidation;
using FluentValidation.Results;
using Library.Application.Common.BaseClasses;

namespace Library.Application.Features.Validators
{
	public static class GenericValidator
	{
		public static List<string> ValidateQuery(Type validatorType, object request)
		{
			var validator = Activator.CreateInstance(validatorType);

			var validateMethod = validatorType.GetMethods()[6];

			var result = validateMethod.Invoke(validator, [request]) as ValidationResult;

			if (result.IsValid)
				return null;
			return result.Errors.Select(e => e.ErrorMessage).ToList();
		}
	}
}
