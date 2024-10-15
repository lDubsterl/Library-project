using FluentValidation;
using Library.Application.Features.Authors.Commands;
using Library.Domain.Common;

namespace Library.Application.Common.Validators
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
