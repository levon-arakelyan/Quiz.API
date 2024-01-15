using Microsoft.Extensions.Configuration;
using Quiz.Core.Interfaces.Infrastructure;
using Quiz.Core.Interfaces.UserManagement;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

var drTypes = new[]
{
    typeof(Quiz.Data.DependencyRegistrar),
    typeof(Quiz.Services.DependencyRegistrar),
};
foreach (var drType in drTypes)
{
    var dependencyRegistrar = (IDependencyRegistrar)Activator.CreateInstance(drType);
    dependencyRegistrar?.Register(builder.Services, builder.Configuration);
}

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
