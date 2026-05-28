using Blog.Models;
using Bogus;

namespace Blog.Tests.Builders;

public class BlogPostBuilder : IBuilder<BlogPost>
{
    private static readonly Faker Faker = new Faker("de");
    private static int _currentId = 0;
    private Author? _author;
    private Category? _category;
    private string? _content;
    private string? _title;
    private string? _slug;
    private bool? _isPublished;
    private DateTime? _publishedDate;
    private List<Comment>? _comments;

    public BlogPostBuilder WithAuthor(Author author)
    {
        _author = author;
        return this;
    }

    public BlogPostBuilder WithAuthor(Action<AuthorBuilder> configure)
    {
        var authorBuilder = new AuthorBuilder();
        configure(authorBuilder);
        _author = authorBuilder.Build();
        return this;
    }

    public BlogPostBuilder WithCategory(Category category)
    {
        _category = category;
        return this;
    }

    public BlogPostBuilder WithCategory(Action<CategoryBuilder> configure)
    {
        var categoryBuilder = new CategoryBuilder();
        configure(categoryBuilder);
        _category = categoryBuilder.Build();
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

    public BlogPostBuilder WithComments(IEnumerable<Comment> comments)
    {
        _comments = comments.ToList();
        return this;
    }

    public BlogPostBuilder WithComments(
        Action<CollectionBuilder<CommentBuilder, Comment>> configure
    )
    {
        var commentsBuilder = new CollectionBuilder<CommentBuilder, Comment>();
        configure(commentsBuilder);
        _comments = commentsBuilder.Build().ToList();
        return this;
    }

    public BlogPostBuilder WithPublishedDate(DateTime? publishedDate)
    {
        _publishedDate = publishedDate;
        return this;
    }

    public BlogPost Build()
    {
        return new BlogPost
        {
            Author = _author ?? new AuthorBuilder().Build(),
            Content = _content ?? Faker.Lorem.Paragraphs(3, 5),
            Id = Interlocked.Increment(ref _currentId),
            Title = _title ?? Faker.Lorem.Sentence(5, 10),
            Slug = _slug ?? Faker.Lorem.Slug(),
            Category = _category ?? new CategoryBuilder().Build(),
            IsPublished = _isPublished ?? Faker.Random.Bool(),
            Comments = _comments ?? new CommentBuilder().Build(Faker.Random.Int(0, 5)).ToList(),
            PublishedDate = _publishedDate
        };
    }

    public IEnumerable<BlogPost> Build(int count)
    {
        return Faker.Make(count, _ => Build());
    }

    public static implicit operator BlogPost(BlogPostBuilder builder)
    {
        return builder.Build();
    }
}
