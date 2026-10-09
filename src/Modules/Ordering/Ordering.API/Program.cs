using Ordering.Application.Orders.Commands.PlaceOrder;
using Ordering.Application.Abstractions.Persistence;
using Ordering.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IOrderRepository, InMemoryOrderRepository>();

// MVC / Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MediatR / CQRS
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(
        typeof(PlaceOrderCommand).Assembly);
});

//// Dependency Injection
//builder.Services.AddScoped<
//    IOrderRepository,
//    InMemoryOrderRepository>();

var app = builder.Build();

// HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();