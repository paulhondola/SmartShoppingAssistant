using Microsoft.EntityFrameworkCore;
using SmartShoppingAssistantBackend.BusinessLogic.Services;
using SmartShoppingAssistantBackend.BusinessLogic.Services.Interfaces;
using SmartShoppingAssistantBackend.DataAccess;
using SmartShoppingAssistantBackend.DataAccess.Entities;
using SmartShoppingAssistantBackend.DataAccess.Repositories;
using SmartShoppingAssistantBackend.DataAccess.Seeding;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IRepository<Product>, BaseRepository<Product>>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddDbContext<SmartShoppingAssistantDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("SmartShoppingAssistantContext")
        ?? throw new InvalidOperationException("Connection string 'SmartShoppingAssistantContext' is not configured."),
        npgsql => npgsql.MigrationsHistoryTable("__EFMigrationsHistory", "smart-shopping-assistant")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<SmartShoppingAssistantDbContext>();
    await DataSeeder.SeedAsync(db);
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();