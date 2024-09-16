using FluentValidation;
using Library.Domain.Common;

namespace Library.Application.Validators
{
	public class AuthorValidator : AbstractValidator<BaseAuthor>
	{
		public AuthorValidator()
		{
			RuleFor(e => e.Name).NotEmpty();
			RuleFor(e => e.Surname).NotEmpty();
		}
	}
}
