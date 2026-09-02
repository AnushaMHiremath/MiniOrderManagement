namespace MiniOrderManagement.Application.Commands.Customers;

public class CreateCustomerProfileCommand
{
    public int CustomerId { get; set; }

    public string Address { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;
}