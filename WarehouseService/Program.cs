using Microsoft.EntityFrameworkCore;
using WarehouseService.Database;
using WarehouseService.Models;
using WarehouseService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IItemRepository, ItemRepository>();
builder.Services.AddDbContext<ItemDbContext>(options =>
    options.UseInMemoryDatabase("ItemDb"));

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("OpenCorsPolicy", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Enable the open CORS policy for all endpoints
app.UseCors("OpenCorsPolicy");

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ItemDbContext>();
    dbContext.SeedData();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();