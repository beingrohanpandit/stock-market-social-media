using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Configurations;
using WebApplication1.Data;
using WebApplication1.Dtos.Stock;
using WebApplication1.Interface;
using WebApplication1.Repository;
using WebApplication1.Validations.Stocks;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

await ServiceConfigurations.Configure(builder.Services,configuration);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// It helps to deal with nested json response
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

// Adding Controllers
builder.Services.AddControllers();

// Establishing the database connection.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
