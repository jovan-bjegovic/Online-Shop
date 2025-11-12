using FluentValidation;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.Validators;

public abstract class ProductValidator : AbstractValidator<Product>
{
    protected ProductValidator()
    {
        RuleFor(p => p.Title)
            .NotEmpty().WithMessage("Title is required.");

        RuleFor(p => p.Sku)
            .NotEmpty().WithMessage("SKU is required.");

        RuleFor(p => p.Brand)
            .NotEmpty().WithMessage("Brand is required.");

        RuleFor(p => p.CategoryId)
            .NotEmpty().WithMessage("Category is required.");

        RuleFor(p => p.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");
    }
}