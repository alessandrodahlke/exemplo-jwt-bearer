using JwtBearer.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<TokenService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapGet("/token", ([FromServices] TokenService tokenService)
=> tokenService.Generate(new JwtBearer.Models.User(
    1, "alessandrodahlke@gmail.com", "123", new string[] { "Admin", "User" })));

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
