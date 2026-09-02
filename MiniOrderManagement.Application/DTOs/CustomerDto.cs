namespace MiniOrderManagement.Application.DTOs;

public class CustomerDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public CustomerProfileDto? Profile { get; set; }

    public List<OrderDto> Orders { get; set; } = new();
}