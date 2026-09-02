using FluentValidation;
using MiniOrderManagement.Application.Commands.Orders;
using MiniOrderManagement.Application.Interfaces;
using MiniOrderManagement.Domain.Entities;

namespace MiniOrderManagement.Application.Handlers.Orders;

public class CreateOrderHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateOrderCommand> _validator;

    public CreateOrderHandler(
        IUnitOfWork unitOfWork,
        IValidator<CreateOrderCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<int> Handle(CreateOrderCommand command)
    {
        await _validator.ValidateAndThrowAsync(command);

        var customer = await _unitOfWork.Customers
            .GetByIdAsync(command.CustomerId);

        if (customer == null)
        {
            throw new KeyNotFoundException(
                $"Customer with ID {command.CustomerId} does not exist.");
        }

        var order = new Order
        {
            CustomerId = command.CustomerId,
            OrderDate = command.OrderDate,
            TotalAmount = command.TotalAmount
        };

        await _unitOfWork.Orders.AddAsync(order);

        await _unitOfWork.SaveChangesAsync();

        return order.Id;
    }
}