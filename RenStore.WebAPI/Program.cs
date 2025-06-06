using RenStore.Domain.Entities;
using RenStore.Application;
using RenStore.Application.Repository;
using RenStore.Persistence;
using RenStore.Persistence.Repository;
using RenStore.Identity.DuendeServer.WebAPI.Data;
using RenStore.Identity.DuendeServer.WebAPI.Data.IdentityConfigurations;
using RenStore.Identity.DuendeServer.WebAPI.Extensions;
using Asp.Versioning;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RenStore.Application.BackgroundServices;
using RenStore.Application.BackgroundsTasks;
using RenStore.Application.Interfaces;
using RenStore.Application.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddApiAuthentication();
builder.Services.AddApplication();
builder.Services.AddPersistence(configuration);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddDbContext<AuthDbContext>(optoins =>
{
    optoins.UseNpgsql(connectionString);
});

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddSignInManager<SignInManager<ApplicationUser>>()
    .AddUserManager<UserManager<ApplicationUser>>()
    .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); 

builder.Services.AddIdentityServer(options =>
{
    options.Authentication.CookieLifetime = TimeSpan.FromHours(10);
});

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader()); 
})
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'V";
        options.SubstituteApiVersionInUrl = true;
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<JwtProvider>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<ISellerRepository, SellerRepository>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>();

builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<ReviewService>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<OrderService>();

builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
builder.Services.AddScoped<ShoppingCartService>();

/*builder.Services.AddHostedService<PriceCounterBackgroundService>();*/

/*builder.Services.AddSingleton<IBackgroundTaskQueue>(_ =>
    new BackgroundTaskQueue(
        capacity: 1000,
        logger: _.GetRequiredService<ILogger<BackgroundTaskQueue>>()));*/

var app = builder.Build();

app.UseRouting();
app.UseHttpsRedirection();

app.UseHsts();
app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(config =>
    {
        config.RoutePrefix = string.Empty;
        config.SwaggerEndpoint("swagger/v1/swagger.json", "Shop API");
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();