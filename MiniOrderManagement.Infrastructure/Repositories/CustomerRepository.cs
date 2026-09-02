using Microsoft.EntityFrameworkCore;
using MiniOrderManagement.Application.Interfaces;
using MiniOrderManagement.Domain.Entities;
using MiniOrderManagement.Infrastructure.Persistence;

namespace MiniOrderManagement.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        return await _context.Customers
            .Include(c => c.Profile)
            .Include(c => c.Orders)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
    }
}