using Blog.Models;

namespace Blog.Tests.TestDataBuilders;

public class BlogPostTestDataBuilder
{
    private int _id = 1;
    private string _title = "Getting Started with C#";
    private string _content = "This is a comprehensive guide to learning C# from the ground up.";
    private DateTime? _publishedDate = new DateTime(2024, 5, 1, 10, 0, 0);
    private Author _author = new AuthorTestDataBuilder().Build();
    private Category? _category = new CategoryTestDataBuilder().Build();
    private bool _isPublished = true;
    private IEnumerable<Comment> _comments = [];

    public BlogPostTestDataBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public BlogPostTestDataBuilder WithTitle(string title)
    {
        _title = title;
        return this;
    }

    public BlogPostTestDataBuilder WithContent(string content)
    {
        _content = content;
        return this;
    }

    public BlogPostTestDataBuilder WithPublishedDate(DateTime? publishedDate)
    {
        _publishedDate = publishedDate;
        return this;
    }

    public BlogPostTestDataBuilder WithAuthor(Author author)
    {
        _author = author;
        return this;
    }

    public BlogPostTestDataBuilder WithAuthor(AuthorTestDataBuilder authorBuilder)
    {
        _author = authorBuilder.Build();
        return this;
    }

    public BlogPostTestDataBuilder WithCategory(Category? category)
    {
        _category = category;
        return this;
    }

    public BlogPostTestDataBuilder WithCategory(CategoryTestDataBuilder categoryBuilder)
    {
        _category = categoryBuilder.Build();
        return this;
    }

    public BlogPostTestDataBuilder WithoutCategory()
    {
        _category = null;
        return this;
    }

    public BlogPostTestDataBuilder WithIsPublished(bool isPublished)
    {
        _isPublished = isPublished;
        return this;
    }

    public BlogPostTestDataBuilder WithComments(IEnumerable<Comment> comments)
    {
        _comments = comments;
        return this;
    }

    public BlogPostTestDataBuilder WithComments(params Comment[] comments)
    {
        _comments = comments;
        return this;
    }

    public BlogPost Build()
    {
        return new BlogPost
        {
            Id = _id,
            Title = _title,
            Content = _content,
            PublishedDate = _publishedDate,
            Author = _author,
            Category = _category,
            IsPublished = _isPublished,
            Comments = _comments,
        };
    }
}
