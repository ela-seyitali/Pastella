using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pastella.Backend.Application.Services;
using Pastella.Backend.Core.Interfaces;
using Pastella.Backend.Infrastructure.Data;
using Pastella.Backend.Infrastructure.Repositories;
using Pastella.Backend.WebAPI.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services using extension methods
builder.Services
    .AddDatabase(builder.Configuration)
    .AddRepositories()
    .AddApplicationServices()
    .AddJwtAuthentication(builder.Configuration)
    .AddApiServices();

var app = builder.Build();

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger().UseSwaggerUI();
}

app.UseHttpsRedirection()
   .UseCors("AllowAll")
   .UseAuthentication()
   .UseAuthorization();

app.MapControllers();
app.Run();
