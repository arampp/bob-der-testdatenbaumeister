using Blog.Models;
using Bogus;

namespace Blog.Tests.Builders;

public class ReplyBuilder : IBuilder<Reply>
{
    private static readonly Faker Faker = new Faker("de");
    private static int _currentId = 0;
    private string? _content;
    private string? _userName;
    private DateTime? _time;

    public ReplyBuilder WithContent(string content)
    {
        _content = content;
        return this;
    }

    public ReplyBuilder WithUserName(string userName)
    {
        _userName = userName;
        return this;
    }

    public ReplyBuilder WithTime(DateTime time)
    {
        _time = time;
        return this;
    }

    public Reply Build()
    {
        return new Reply
        {
            Id = Interlocked.Increment(ref _currentId),
            Content = _content ?? Faker.Lorem.Sentence(),
            UserName = _userName ?? Faker.Name.FullName(),
            Time = _time ?? Faker.Date.Recent(),
        };
    }

    internal IEnumerable<Reply> Build(int count)
    {
        return Faker.Make(count, _ => Build());
    }
}
