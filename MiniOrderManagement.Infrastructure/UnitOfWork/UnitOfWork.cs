using MiniOrderManagement.Application.Interfaces;
using MiniOrderManagement.Infrastructure.Persistence;
using MiniOrderManagement.Infrastructure.Repositories;

namespace MiniOrderManagement.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public ICustomerRepository Customers { get; }

    public IOrderRepository Orders { get; }

    public ICustomerProfileRepository CustomerProfiles { get; }

    public UnitOfWork(
        AppDbContext context,
        ICustomerRepository customerRepository,
        IOrderRepository orderRepository,
        ICustomerProfileRepository customerProfileRepository)
    {
        _context = context;

        Customers = customerRepository;
        Orders = orderRepository;
        CustomerProfiles = customerProfileRepository;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}