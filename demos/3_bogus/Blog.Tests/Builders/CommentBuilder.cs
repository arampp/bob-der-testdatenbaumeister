using Blog.Models;
using Bogus;

namespace Blog.Tests.Builders;

public class CommentBuilder : IBuilder<Comment>
{
    private static readonly Faker Faker = new Faker("de");
    private static int _currentId = 0;
    private string? _userName;
    private string? _text;
    private DateTime? _time;
    private List<Reply>? _replies;

    public CommentBuilder WithUserName(string userName)
    {
        _userName = userName;
        return this;
    }

    public CommentBuilder WithText(string text)
    {
        _text = text;
        return this;
    }

    public CommentBuilder WithTime(DateTime time)
    {
        _time = time;
        return this;
    }

    public CommentBuilder WithReplies(Action<CollectionBuilder<ReplyBuilder, Reply>> configure)
    {
        var collectionBuilder = new CollectionBuilder<ReplyBuilder, Reply>();
        configure(collectionBuilder);
        _replies = collectionBuilder.Build().ToList();
        return this;
    }

    public Comment Build()
    {
        return new Comment
        {
            UserName = _userName ?? Faker.Name.FullName(),
            Text = _text ?? Faker.Lorem.Sentence(),
            Time = _time ?? Faker.Date.Recent(),
            Replies = _replies ?? new ReplyBuilder().Build(Faker.Random.Int(0, 3)).ToList(),
            Id = Interlocked.Increment(ref _currentId),
        };
    }

    internal IEnumerable<Comment> Build(int count)
    {
        return Faker.Make(count, _ => Build());
    }
}
