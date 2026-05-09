using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using OpenAI;
using Backend.BusinessLogic.Services;
using Backend.BusinessLogic.Services.Interfaces;
using Backend.DataAccess;
using Backend.DataAccess.Entities;
using Backend.DataAccess.Repositories;
using Backend.DataAccess.Repositories.Interfaces;
using Backend.DataAccess.Seeding;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Product
builder.Services.AddScoped<IRepository<Product>, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

// Categories
builder.Services.AddScoped<IRepository<Category>, BaseRepository<Category>>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// Promotions
builder.Services.AddScoped<IRepository<Promotion>, BaseRepository<Promotion>>();
builder.Services.AddScoped<IPromotionService, PromotionService>();

// Cart
builder.Services.AddScoped<IRepository<Cart>, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();

// Agent
var openAiApiKey = builder.Configuration["OpenAiApiKey"] ??
                   throw new InvalidOperationException("Open AI API Key is not configured.");
var openAiModel = builder.Configuration["OpenAiModel"] ?? "gpt-4o";

builder.Services.AddSingleton(new OpenAIClient(openAiApiKey)
    .GetChatClient(openAiModel)
    .AsIChatClient()
    .AsBuilder()
    .UseFunctionInvocation()
    .Build());

builder.Services.AddScoped<IPromotionCheckerAgent, PromotionCheckerAgent>();

builder.Services.AddDbContext<BackendDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("BackendContext")
        ?? throw new InvalidOperationException("Connection string 'BackendContext' is not configured."),
        npgsql => npgsql.MigrationsHistoryTable("__EFMigrationsHistory", "backend")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<BackendDbContext>();
    await DataSeeder.SeedAsync(db);
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();