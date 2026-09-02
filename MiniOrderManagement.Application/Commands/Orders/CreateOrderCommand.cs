namespace MiniOrderManagement.Application.Commands.Orders;

public class CreateOrderCommand
{
    public int CustomerId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }
}