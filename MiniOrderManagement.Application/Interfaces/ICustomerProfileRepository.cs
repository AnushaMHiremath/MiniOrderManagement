using MiniOrderManagement.Domain.Entities;

namespace MiniOrderManagement.Application.Interfaces;

public interface ICustomerProfileRepository
{
    Task AddAsync(CustomerProfile profile);

    Task<CustomerProfile?> GetByCustomerIdAsync(int customerId);
}