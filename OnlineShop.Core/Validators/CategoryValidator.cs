using FluentValidation;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.Validators;

public abstract class CategoryValidator : AbstractValidator<Category>
{
    protected CategoryValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty().WithMessage("Title is required.");

        RuleFor(c => c.Code)
            .NotEmpty().WithMessage("Code is required.")
            .WithMessage("Code must be unique.");
    }
}