using Microsoft.EntityFrameworkCore;

namespace MicrosoftAzureForDevelopers.Model;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Person> Persons { get; set; }
}