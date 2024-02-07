using Microsoft.EntityFrameworkCore;
using webAPI_demo1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var provider = builder.Services.BuildServiceProvider();
var config = provider.GetRequiredService<IConfiguration>();
builder.Services.AddDbContext<MylocalDatabaseContext>(item => item.UseSqlServer(config.GetConnectionString("dbcs")));
var app = builder.Build();

app.UseCors(x => x.AllowAnyMethod()
                  .AllowAnyHeader()
                  .WithOrigins("http://localhost:4200", "http://192.168.0.76:3000"));

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
