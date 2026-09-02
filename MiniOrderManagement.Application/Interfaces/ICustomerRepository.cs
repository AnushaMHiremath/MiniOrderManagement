using MiniOrderManagement.Domain.Entities;

namespace MiniOrderManagement.Application.Interfaces;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(int id);

    Task AddAsync(Customer customer);
}