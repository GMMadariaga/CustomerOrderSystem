using CustomerOrderSystem.Domain.Interfaces;
using CustomerOrderSystem.Infrastructure.Data;
using CustomerOrderSystem.Infrastructure.Repositories;
using CustomerOrderSystem.Application.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database Context - usando InMemory para desarrollo
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("CustomerOrderDb"));

// Repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Application Services
builder.Services.AddScoped<OrderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();