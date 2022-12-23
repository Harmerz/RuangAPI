using Microsoft.EntityFrameworkCore;
using RuangAPI.Data;
using Pomelo.EntityFrameworkCore.MySql;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "103.167.132.107";
var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "ruang";
var dbPassword = Environment.GetEnvironmentVariable("DB_ROOT_PASSWORD") ?? "DTIhxh19463";
var connectionString = $"server={dbHost};port=3306;database={dbName};user=root;password={dbPassword}";
builder.Services.AddDbContext<APIContext>(options =>
    {
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI( c=>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ruang API V1");
        c.RoutePrefix = string.Empty;
    });
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ruang API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
