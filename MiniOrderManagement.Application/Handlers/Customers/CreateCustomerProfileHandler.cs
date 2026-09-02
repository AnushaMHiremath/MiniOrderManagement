using FluentValidation;
using MiniOrderManagement.Application.Commands.Customers;
using MiniOrderManagement.Application.Interfaces;
using MiniOrderManagement.Domain.Entities;

namespace MiniOrderManagement.Application.Handlers.Customers;

public class CreateCustomerProfileHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateCustomerProfileCommand> _validator;

    public CreateCustomerProfileHandler(
        IUnitOfWork unitOfWork,
        IValidator<CreateCustomerProfileCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<int> Handle(CreateCustomerProfileCommand command)
    {
        await _validator.ValidateAndThrowAsync(command);

        var customer = await _unitOfWork.Customers
            .GetByIdAsync(command.CustomerId);

        if (customer == null)
        {
            throw new KeyNotFoundException(
                $"Customer with ID {command.CustomerId} does not exist.");
        }

        var profile = new CustomerProfile
        {
            CustomerId = command.CustomerId,
            Address = command.Address,
            PhoneNumber = command.PhoneNumber
        };

        await _unitOfWork.CustomerProfiles.AddAsync(profile);

        await _unitOfWork.SaveChangesAsync();

        return profile.Id;
    }
}