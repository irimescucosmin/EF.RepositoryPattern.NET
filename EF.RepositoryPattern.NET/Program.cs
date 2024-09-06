using EF.RepositoryPattern.NET.Contexts;
using EF.RepositoryPattern.NET.Interfaces;
using EF.RepositoryPattern.NET.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<CustomersDbContext>(options =>
    options.UseSqlite("Data Source=Customers.db;"));

builder.Services.AddScoped(typeof(ICustomersRepository<>), typeof(CustomersRepository<>));

var app = builder.Build();

// ensure migration is done
using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetService<CustomersDbContext>();
    context!.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

await app.RunAsync();