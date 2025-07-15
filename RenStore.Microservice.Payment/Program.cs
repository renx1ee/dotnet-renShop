using Microsoft.EntityFrameworkCore;
using RenStore.Microservice.Payment;
using RenStore.Microservice.Payment.Endpoints;
using RenStore.Microservice.Payment.Repository;
using RenStore.Microservice.Payment.Senders;
using RenStore.Microservice.Payment.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpClient();

var connectionString = builder.Configuration.GetConnectionString("PaymentConnection");

builder.Services.AddDbContext<PaymentDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<ITinkoffSBPSender, TinkoffSBPSender>();
builder.Services.AddScoped<ITinkoffSBPService, TinkoffSBPService>();
builder.Services.AddScoped<PaymentSBPService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(config =>
    {
        config.RoutePrefix = string.Empty;
        config.SwaggerEndpoint("swagger/v1/swagger.json", "Shop API");
    });
}

app.UseHttpsRedirection();
app.MapPaymentEndpoints();

app.Run();