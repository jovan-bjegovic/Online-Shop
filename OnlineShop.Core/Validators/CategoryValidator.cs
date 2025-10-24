using FluentValidation;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.Validators;

public abstract class CategoryValidator : AbstractValidator<Category>
{
    protected CategoryValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(50).WithMessage("Title cannot exceed 50 characters.");

        RuleFor(c => c.Code)
            .NotEmpty().WithMessage("Code is required.")
            .Length(2, 20).WithMessage("Code must be between 2 and 20 characters.")
            .WithMessage("Code must be unique.");

        RuleFor(c => c.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
    }
}