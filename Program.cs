using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyHotelApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IHotelService, HotelService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();              
    app.UseSwaggerUI();            
}


app.MapControllers();

app.Run();
