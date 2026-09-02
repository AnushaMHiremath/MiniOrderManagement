using FluentValidation;
using MiniOrderManagement.Application.Commands.Customers;
using MiniOrderManagement.Application.Interfaces;
using MiniOrderManagement.Domain.Entities;

namespace MiniOrderManagement.Application.Handlers.Customers;

public class CreateCustomerHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateCustomerCommand> _validator;

    public CreateCustomerHandler(
        IUnitOfWork unitOfWork,
        IValidator<CreateCustomerCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<int> Handle(CreateCustomerCommand command)
    {
        await _validator.ValidateAndThrowAsync(command);

        var customer = new Customer
        {
            Name = command.Name
        };

        await _unitOfWork.Customers.AddAsync(customer);

        await _unitOfWork.SaveChangesAsync();

        return customer.Id;
    }
}