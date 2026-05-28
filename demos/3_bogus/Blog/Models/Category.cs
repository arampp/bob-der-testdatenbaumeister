namespace Blog.Models;

public class Category
{
    public required int Id { get; init; }

    public required string Name { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;
}
