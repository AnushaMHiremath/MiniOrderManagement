using Microsoft.EntityFrameworkCore;
using MiniOrderManagement.Application.Interfaces;
using MiniOrderManagement.Domain.Entities;
using MiniOrderManagement.Infrastructure.Persistence;

namespace MiniOrderManagement.Infrastructure.Repositories;

public class CustomerProfileRepository : ICustomerProfileRepository
{
    private readonly AppDbContext _context;

    public CustomerProfileRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(CustomerProfile profile)
    {
        await _context.CustomerProfiles.AddAsync(profile);
    }

    public async Task<CustomerProfile?> GetByCustomerIdAsync(int customerId)
    {
        return await _context.CustomerProfiles
            .FirstOrDefaultAsync(x => x.CustomerId == customerId);
    }
}