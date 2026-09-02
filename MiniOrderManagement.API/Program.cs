using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MiniOrderManagement.API.Middleware;
using MiniOrderManagement.Application.Handlers.Customers;
using MiniOrderManagement.Application.Handlers.Orders;
using MiniOrderManagement.Application.Interfaces;
using MiniOrderManagement.Application.Queries.Customers;
using MiniOrderManagement.Application.Queries.Orders;
using MiniOrderManagement.Infrastructure.Persistence;
using MiniOrderManagement.Infrastructure.Repositories;
using MiniOrderManagement.Infrastructure.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssemblyContaining<
    MiniOrderManagement.Application.Validators.Customers.CreateCustomerCommandValidator>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();


builder.Services.AddScoped<ICustomerProfileRepository, CustomerProfileRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<CreateCustomerHandler>();
builder.Services.AddScoped<GetCustomerByIdHandler>();
builder.Services.AddScoped<CreateCustomerProfileHandler>();

builder.Services.AddScoped<CreateOrderHandler>();
builder.Services.AddScoped<GetOrdersByCustomerIdHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

app.UseMiddleware<ExceptionHandlingMiddleware>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
