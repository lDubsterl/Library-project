using FluentValidation;
using Library.Application.DTOs;

namespace Library.Application.Features.Validators
{
    public class PaginationValidator : AbstractValidator<PaginationBase>
    {
        public PaginationValidator()
        {
            RuleFor(e => e.PageNumber).LessThanOrEqualTo(1)
                .WithMessage("Page number should be at least greater than or equal to 1.");
            RuleFor(e => e.PageSize).LessThanOrEqualTo(1)
                .WithMessage("Page size should be at least greater than or equal to 1.");
        }
    }
}
