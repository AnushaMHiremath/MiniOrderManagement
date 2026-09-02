using FluentValidation;
using MiniOrderManagement.Application.Commands.Customers;

namespace MiniOrderManagement.Application.Validators.Customers;

public class CreateCustomerCommandValidator
    : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Customer name is required.")
            .MaximumLength(100)
            .WithMessage("Customer name cannot exceed 100 characters.");
    }
}