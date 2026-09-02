using Microsoft.EntityFrameworkCore;
using MiniOrderManagement.Application.Interfaces;
using MiniOrderManagement.Domain.Entities;
using MiniOrderManagement.Infrastructure.Persistence;

namespace MiniOrderManagement.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
    }

    public async Task<List<Order>> GetByCustomerIdAsync(int customerId)
    {
        return await _context.Orders
            .Where(o => o.CustomerId == customerId)
            .ToListAsync();
    }
}