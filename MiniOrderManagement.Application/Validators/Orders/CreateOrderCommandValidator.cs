using FluentValidation;
using MiniOrderManagement.Application.Commands.Orders;

namespace MiniOrderManagement.Application.Validators.Orders;

public class CreateOrderCommandValidator
    : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .GreaterThan(0)
            .WithMessage("Customer ID must be greater than zero.");

        RuleFor(x => x.OrderDate)
            .NotEmpty()
            .WithMessage("Order date is required.");

        RuleFor(x => x.TotalAmount)
            .GreaterThan(0)
            .WithMessage("Order total amount must be greater than zero.");
    }
}