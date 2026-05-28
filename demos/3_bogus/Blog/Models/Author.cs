namespace Blog.Models;

public class Author
{
    public required int Id { get; init; }

    public required string Name { get; init; } = string.Empty;

    public required string Email { get; init; } = string.Empty;
}
