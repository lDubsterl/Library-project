using FluentValidation;
using Library.Domain.Common;

namespace Library.Application.Validators
{
	public class BookValidator : AbstractValidator<BaseBook>
	{
		public BookValidator()
		{
			RuleFor(e => e.ISBN)
				.NotEmpty()
				.WithMessage("ISBN cannot be empty");
			RuleFor(e => e.Title)
				.NotEmpty()
				.WithMessage("Title cannot be empty");
		}
	}
}
