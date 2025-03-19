namespace MicrosoftAzureForDevelopers.Model;

public class Person
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
    public required DateTime DateOfBirth { get; set; }
}