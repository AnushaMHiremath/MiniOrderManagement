using MiniOrderManagement.Application.Interfaces;

public interface IUnitOfWork
{
    ICustomerRepository Customers { get; }

    IOrderRepository Orders { get; }

    ICustomerProfileRepository CustomerProfiles { get; }

    Task<int> SaveChangesAsync();
}