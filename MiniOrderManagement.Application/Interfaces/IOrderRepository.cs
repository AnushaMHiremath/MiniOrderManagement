using MiniOrderManagement.Domain.Entities;

namespace MiniOrderManagement.Application.Interfaces;

public interface IOrderRepository
{
    Task AddAsync(Order order);

    Task<List<Order>> GetByCustomerIdAsync(int customerId);
}