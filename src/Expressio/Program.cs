using Microsoft.EntityFrameworkCore;
using Expressio.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<ExpressioContext>(opt => opt.UseInMemoryDatabase("Expressio"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(ExpressioProfile));

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();