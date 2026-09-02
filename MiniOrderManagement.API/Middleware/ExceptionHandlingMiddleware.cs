using FluentValidation;
using System.Net;
using System.Text.Json;

namespace MiniOrderManagement.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            var response = new
            {
                message = "Validation failed.",
                errors = ex.Errors.Select(error => new
                {
                    field = error.PropertyName,
                    message = error.ErrorMessage
                })
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
        catch (KeyNotFoundException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            context.Response.ContentType = "application/json";

            var response = new
            {
                message = ex.Message
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
        catch (Exception)
        {
            context.Response.StatusCode =
                (int)HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            var response = new
            {
                message = "An unexpected error occurred."
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}