using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MicrosoftAzureForDevelopers.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionString = builder.Configuration.GetConnectionString("AzureSQLConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapGet("/persons", (AppDbContext db) => db.Persons.ToList());

app.MapGet("/", () => "Hello World! " + app.Environment.EnvironmentName);

app.UseHttpsRedirection();

app.Run();