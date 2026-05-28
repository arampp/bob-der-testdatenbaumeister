using System;

namespace Blog.Models;

public class Reply
{
    public required int Id { get; init; }
    public required string Content { get; init; }
    public required string UserName { get; init; }
    public DateTime Time { get; init; }
}
