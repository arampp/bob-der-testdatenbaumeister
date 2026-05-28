using Blog.Models;

namespace Blog.Tests.Builders;

public class BlogPostBuilder : IBuilder<BlogPost>
{
    private Author _author = new AuthorBuilder().Build();
    private Category _category = new CategoryBuilder().Build();
    private string _content = "Hey there, last week I attended a conference about the future of technology and I was " +
     "blown away by the innovations that are coming our way. From AI to quantum computing, the possibilities are " +
     "endless. I can't wait to see how these technologies will shape our world in the next few years.";
    private string _title = "The next big thing in tech.";
    private string _slug = "the-next-big-thing-in-tech";
    private bool _isPublished = false;
    private static int _currentId = 0;

    public BlogPostBuilder WithAuthor(Author author)
    {
        _author = author;
        return this;
    }

    public BlogPostBuilder WithCategory(Category category)
    {
        _category = category;
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

    public BlogPostBuilder WithIsPublished(bool isPublished)
    {
        _isPublished = isPublished;
        return this;
    }

    public BlogPost Build()
    {
        return new BlogPost
        {
            Author = _author,
            Content = _content,
            Id = Interlocked.Increment(ref _currentId),
            Title = _title,
            Slug = _slug,
            Category = _category,
            IsPublished = _isPublished
        };
    }
}
