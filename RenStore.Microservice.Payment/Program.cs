using RenStore.Microservice.Payment.Endpoints;
using RenStore.Microservice.Payment.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.MapPaymentEndpoints();

app.Run();