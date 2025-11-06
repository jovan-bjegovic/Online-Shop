using FluentValidation;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.Validators;

public abstract class UserValidator : AbstractValidator<User>
{
    protected UserValidator()
    {
        RuleFor(u => u.Username)
            .NotEmpty().WithMessage("Username is required.");

        RuleFor(u => u.PasswordHash)
            .NotEmpty().WithMessage("Password is required.");

        RuleFor(u => u.Role)
            .NotEmpty().WithMessage("Role is required.")
            .Must(role => role is "Admin" or "User")
            .WithMessage("Role must be either 'Admin' or 'User'.");
    }
}