using Microsoft.EntityFrameworkCore;
using Company.Data.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


/* Add the necessary service for handling database calls to the SQL
Server using the CourseContext and the connection string. */
builder.Services.AddDbContext<CompanyContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("CompanyConnection")));  // "CompanyConnection" was set in appsettings.json



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
