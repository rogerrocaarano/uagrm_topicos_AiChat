namespace Domain.Service;

public interface IDatabaseSeeder
{
    Task SeedFromFile(string filePath);
    Task SeedFromDirectory(string directoryPath);
}