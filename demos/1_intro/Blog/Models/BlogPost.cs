namespace Blog.Models;

public class BlogPost
{
    public required int Id { get; init; }

    public required string Title { get; init; } = string.Empty;

    public required string Content { get; init; } = string.Empty;

    public DateTime? PublishedDate { get; init; }

    public required Author Author { get; init; }

    public Category? Category { get; init; }

    public bool IsPublished { get; init; }
}
