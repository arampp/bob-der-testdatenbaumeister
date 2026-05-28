using System;

namespace Blog.Models;

public class Comment
{
    public required int Id { get; init; }
    public required string UserName { get; init; }
    public required string Text { get; init; }
    public required DateTime Time { get; init; }
    public IEnumerable<Reply> Replies { get; init; } = [];
}
