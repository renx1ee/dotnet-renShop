using RenStore.Microservice.Cache.Endpoints;
using RenStore.Microservice.Cache.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();

/*builder.Services.AddScoped<DistributedCacheService>();*/
builder.Services.AddScoped<MemoryCacheService>();

builder.Services.AddMemoryCache();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseHttpsRedirection();

app.MapCacheEndpoints();

app.UseSwagger();
app.UseSwaggerUI(config =>
{
    config.RoutePrefix = string.Empty;
    config.SwaggerEndpoint("swagger/v1/swagger.json", "Shop API");
});

app.Run();