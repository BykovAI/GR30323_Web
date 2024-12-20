using GR30323_Web.API.Data;
using Microsoft.EntityFrameworkCore;
using GR30323_Web.Domain.Services.CategoryService;
using GR30323_Web.Domain.Services.ProductService;
using GR30323_Web.API;

var builder = WebApplication.CreateBuilder(args);

var conString = builder.Configuration.GetConnectionString("Default");

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(conString);
}
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// Получение контекста БД 
using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
// Выполнение миграций 
await context.Database.MigrateAsync();

await DbInitializer.SeedData(app);

app.Run();
