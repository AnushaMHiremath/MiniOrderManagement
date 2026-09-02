namespace MiniOrderManagement.Application.DTOs;

public class CustomerProfileDto
{
    public int Id { get; set; }

    public string Address { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;
}