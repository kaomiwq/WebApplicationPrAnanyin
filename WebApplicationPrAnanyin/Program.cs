using Microsoft.EntityFrameworkCore;
using WebApplicationPrAnanyin;
using WebApplicationPrAnanyin.Interface;
using WebApplicationPrAnanyin.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectingString = builder.Configuration.GetConnectionString("ConnectDb");
builder.Services.AddDbContext<AnanyinReservationContext>(options => options.UseSqlServer(connectingString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IReservation, ReservationClass>();

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
