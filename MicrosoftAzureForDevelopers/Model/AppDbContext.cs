using Microsoft.EntityFrameworkCore;

namespace MicrosoftAzureForDevelopers.Model;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Person> Persons { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure the 'Person' entity
        modelBuilder.Entity<Person>()
            .Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Person>()
            .Property(p => p.LastName)
            .HasMaxLength(100);

        // Seed data for the 'Person' entity
        modelBuilder.Entity<Person>().HasData(
            new Person { Id = 1, FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(1990, 5, 15) },
            new Person { Id = 2, FirstName = "Jane", LastName = "Smith", DateOfBirth = new DateTime(1985, 3, 22) },
            new Person { Id = 3, FirstName = "Alex", LastName = "Johnson", DateOfBirth = new DateTime(2000, 7, 10) },
            new Person { Id = 4, FirstName = "Emily", LastName = "Davis", DateOfBirth = new DateTime(1992, 11, 4) },
            new Person { Id = 5, FirstName = "Michael", LastName = "Brown", DateOfBirth = new DateTime(1980, 8, 30) }
        );
    }
}