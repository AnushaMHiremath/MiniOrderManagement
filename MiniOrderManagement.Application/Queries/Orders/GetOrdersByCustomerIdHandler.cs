using MiniOrderManagement.Application.DTOs;
using MiniOrderManagement.Application.Interfaces;

namespace MiniOrderManagement.Application.Queries.Orders;

public class GetOrdersByCustomerIdHandler
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOrdersByCustomerIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<OrderDto>> Handle(
        GetOrdersByCustomerIdQuery query)
    {
        var orders = await _unitOfWork.Orders
            .GetByCustomerIdAsync(query.CustomerId);

        return orders
            .Select(order => new OrderDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount
            })
            .ToList();
    }
}