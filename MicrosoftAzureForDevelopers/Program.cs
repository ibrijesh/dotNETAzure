using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Options;
using MicrosoftAzureForDevelopers.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


if (builder.Environment.IsDevelopment())
{
    var connectionString = builder.Configuration.GetConnectionString("DevAzureSQLConnection");
    Console.WriteLine(connectionString);
    builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
}


if (builder.Environment.IsProduction())
{
    // Azure Key Vault URL
    var keyVaultUrl = builder.Configuration.GetValue<string>("KeyVault:KeyVaultUrl");

    // Create a Secret Client (Uses Managed Identity / Default Azure Credential)
    var secretClient = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());

    // Fetch Azure SQL Connection String from Key Vault
    KeyVaultSecret secret = secretClient.GetSecret("ProdAzureSQLConnection");
    var connectionString = secret.Value;
    builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
}


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