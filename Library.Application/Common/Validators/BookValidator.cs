using FluentValidation;
using Library.Application.Features.Books.Commands;
using Library.Domain.Common;

namespace Library.Application.Common.Validators
{
    public class BookValidator : AbstractValidator<BaseBook>
    {
        public BookValidator()
        {
            RuleFor(e => e.ISBN)
                .NotEmpty()
				.Matches("^(?=(?:[^0-9]*[0-9]){10}(?:(?:[^0-9]*[0-9]){3})?$)[\\d-]+$")
				.WithMessage("Invalid ISBN format");
			RuleFor(e => e.Title)
                .NotEmpty()
                .WithMessage("Title cannot be empty");
        }
    }
}
