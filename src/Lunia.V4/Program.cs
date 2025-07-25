using Lunia.V4.Application;
using Lunia.V4.Endpoints;
using Lunia.V4.Helpers;
using Lunia.V4.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddRequestParsing();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapLobbyEndpoints();
app.UseHttpsRedirection();
app.Run();