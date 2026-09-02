using Microsoft.AspNetCore.Mvc;
using MiniOrderManagement.Application.Commands.Customers;
using MiniOrderManagement.Application.Handlers.Customers;
using MiniOrderManagement.Application.Queries.Customers;

namespace MiniOrderManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly CreateCustomerHandler _createCustomerHandler;
    private readonly GetCustomerByIdHandler _getCustomerByIdHandler;

    public CustomersController(
        CreateCustomerHandler createCustomerHandler,
        GetCustomerByIdHandler getCustomerByIdHandler)
    {
        _createCustomerHandler = createCustomerHandler;
        _getCustomerByIdHandler = getCustomerByIdHandler;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer(
        CreateCustomerCommand command)
    {
        var customerId = await _createCustomerHandler.Handle(command);

        return CreatedAtAction(
            nameof(GetCustomerById),
            new { id = customerId },
            new { id = customerId });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById(int id)
    {
        var query = new GetCustomerByIdQuery
        {
            CustomerId = id
        };

        var customer = await _getCustomerByIdHandler.Handle(query);

        if (customer == null)
        {
            return NotFound();
        }

        return Ok(customer);
    }
}