using MiniOrderManagement.Application.DTOs;
using MiniOrderManagement.Application.Interfaces;
using MiniOrderManagement.Application.Queries.Customers;

namespace MiniOrderManagement.Application.Queries.Customers;

public class GetCustomerByIdHandler
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCustomerByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomerDto?> Handle(
        GetCustomerByIdQuery query)
    {
        var customer = await _unitOfWork.Customers
            .GetByIdAsync(query.CustomerId);

        if (customer == null)
        {
            return null;
        }

        return new CustomerDto
        {
            Id = customer.Id,
            Name = customer.Name,

            Profile = customer.Profile == null
                ? null
                : new CustomerProfileDto
                {
                    Id = customer.Profile.Id,
                    Address = customer.Profile.Address,
                    PhoneNumber = customer.Profile.PhoneNumber
                },

            Orders = customer.Orders
                .Select(order => new OrderDto
                {
                    Id = order.Id,
                    CustomerId = order.CustomerId,
                    OrderDate = order.OrderDate,
                    TotalAmount = order.TotalAmount
                })
                .ToList()
        };
    }
}