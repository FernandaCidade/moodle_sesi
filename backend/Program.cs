using Microsoft.EntityFrameworkCore;
using SesiCursos.Api.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection não configurada.");

// MySQL: "8.0.36-mysql"  |  MariaDB: "10.11.6-mariadb"
var serverVersion = ServerVersion.Parse(builder.Configuration["Database:ServerVersion"] ?? "8.0.36-mysql");

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseMySql(connectionString, serverVersion));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(o => o.AddDefaultPolicy(p =>
    p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.Run();