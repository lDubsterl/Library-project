using FluentValidation;
using Library.Application.Common.BaseClasses;

namespace Library.Application.Common.Validators
{
    public class PaginationValidator : AbstractValidator<PaginationBase>
    {
        public PaginationValidator()
        {
            RuleFor(e => e.PageNumber).GreaterThanOrEqualTo(1)
                .WithMessage("Page number should be at least greater than or equal to 1.");
            RuleFor(e => e.PageSize).GreaterThanOrEqualTo(1)
                .WithMessage("Page size should be at least greater than or equal to 1.");
        }
    }
}
