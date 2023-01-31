using Microsoft.EntityFrameworkCore;
using pizza_orders.data;
using pizza_orders.data.Repositories;
using pizza_orders.data.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string dbConnection = builder.Configuration.GetConnectionString("ApplicationDefault");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(dbConnection));
builder.Services.AddScoped<IPizzaRepository, PizzaRepository>();

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

app.Run();
