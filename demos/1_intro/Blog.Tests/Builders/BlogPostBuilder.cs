using Blog.Models;

namespace Blog.Tests.Builders;

public class BlogPostBuilder : IBuilder<BlogPost>
{
    private Author _author = new()
    {
        Id = 1,
        Name = "Max Mustermann",
        Email = "max.mustermann@example.com",
    };
    private string _content =
        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";
    private string _title = "Lorem Ipsum";
    private string _slug = "lorem-ipsum";
    private DateTime? _publishedDate = null;
    private Category? _category = null;
    private bool _isPublished = false;
    private static int _nextId = 1;

    public BlogPostBuilder WithAuthor(Author author)
    {
        _author = author;
        return this;
    }

    public BlogPostBuilder WithContent(string content)
    {
        _content = content;
        return this;
    }

    public BlogPostBuilder WithTitle(string title)
    {
        _title = title;
        return this;
    }

    public BlogPostBuilder WithSlug(string slug)
    {
        _slug = slug;
        return this;
    }

    public BlogPostBuilder WithPublishedDate(DateTime? publishedDate)
    {
        _publishedDate = publishedDate;
        return this;
    }

    public BlogPostBuilder WithCategory(Category? category)
    {
        _category = category;
        return this;
    }

    public BlogPostBuilder WithIsPublished(bool isPublished)
    {
        _isPublished = isPublished;
        return this;
    }

    public BlogPost Build()
    {
        return new BlogPost
        {
            Id = Interlocked.Increment(ref _nextId),
            Author = _author,
            Content = _content,
            Title = _title,
            PublishedDate = _publishedDate,
            Category = _category,
            IsPublished = _isPublished,
        };
    }
}
