using Microsoft.AspNetCore.Mvc;
using MiniOrderManagement.Application.Commands.Customers;
using MiniOrderManagement.Application.Handlers.Customers;

namespace MiniOrderManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerProfilesController : ControllerBase
{
    private readonly CreateCustomerProfileHandler _handler;

    public CustomerProfilesController(
        CreateCustomerProfileHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProfile(
        CreateCustomerProfileCommand command)
    {
        try
        {
            var profileId = await _handler.Handle(command);

            return Created(
                $"/api/CustomerProfiles/{profileId}",
                new { id = profileId });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new
            {
                message = ex.Message
            });
        }
    }
}