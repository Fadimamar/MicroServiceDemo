using System.Runtime.ConstrainedExecution;
using System.Reflection.Metadata.Ecma335;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<AppDbContext>(options =>
  //  options.UseSqlServer(builder.Configuration.GetConnectionString("WebApplication2Context")));
builder.Services.AddDbContext<AppDbContext>(opt=>opt.UseInMemoryDatabase("InMem"));
builder.Services.AddScoped<IPlatformRepo,PlatformRepo>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();
PlatformService.Data.PrebData.PrebPopulation(app);
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
