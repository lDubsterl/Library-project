using FluentValidation;

namespace Library.Application.Features.Validators
{
	public static class GenericValidator
	{
		public static List<string>? ValidateQuery<TQuery>(Type validatorType, TQuery request)
		{
			var validator = validatorType.GetConstructor([])?.Invoke(null);
			var realValidator = validator as AbstractValidator<TQuery>;
			var result = realValidator?.Validate(request)!;
			if (result.IsValid)
				return null;
			return result.Errors.Select(e => e.ErrorMessage).ToList();
		}
	}
}
