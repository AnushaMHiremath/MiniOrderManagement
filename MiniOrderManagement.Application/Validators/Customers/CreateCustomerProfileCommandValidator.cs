using FluentValidation;
using MiniOrderManagement.Application.Commands.Customers;

namespace MiniOrderManagement.Application.Validators.Customers;

public class CreateCustomerProfileCommandValidator
    : AbstractValidator<CreateCustomerProfileCommand>
{
    public CreateCustomerProfileCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .GreaterThan(0)
            .WithMessage("Customer ID must be greater than zero.");

        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage("Address is required.")
            .MaximumLength(250)
            .WithMessage("Address cannot exceed 250 characters.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required.")
            .MaximumLength(20)
            .WithMessage("Phone number cannot exceed 20 characters.");
    }
}