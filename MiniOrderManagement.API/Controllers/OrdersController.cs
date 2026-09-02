using Microsoft.AspNetCore.Mvc;
using MiniOrderManagement.Application.Commands.Orders;
using MiniOrderManagement.Application.Handlers.Orders;
using MiniOrderManagement.Application.Queries.Orders;

namespace MiniOrderManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly CreateOrderHandler _createOrderHandler;
    private readonly GetOrdersByCustomerIdHandler _getOrdersByCustomerIdHandler;

    public OrdersController(
        CreateOrderHandler createOrderHandler,
        GetOrdersByCustomerIdHandler getOrdersByCustomerIdHandler)
    {
        _createOrderHandler = createOrderHandler;
        _getOrdersByCustomerIdHandler = getOrdersByCustomerIdHandler;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(
    CreateOrderCommand command)
    {
        try
        {
            var orderId = await _createOrderHandler.Handle(command);

            return Created(
                $"/api/Orders/{orderId}",
                new { id = orderId });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new
            {
                message = ex.Message
            });
        }
    }

    [HttpGet("customer/{customerId}")]
    public async Task<IActionResult> GetOrdersByCustomerId(
        int customerId)
    {
        var query = new GetOrdersByCustomerIdQuery
        {
            CustomerId = customerId
        };

        var orders = await _getOrdersByCustomerIdHandler.Handle(query);

        return Ok(orders);
    }
}