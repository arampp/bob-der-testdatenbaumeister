using Blog.Models;

namespace Blog.Tests.Builders;

public class CommentBuilder : IBuilder<Comment>
{
    private string _userName = "Jane Smith";
    private string _text = "This is a great article!";
    private DateTime _time = DateTime.Now;
    private IEnumerable<Reply> _replies = [];
    private static int _currentId = 0;

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
        _replies = collectionBuilder.Build();
        return this;
    }

    public Comment Build()
    {
        return new Comment
        {
            UserName = _userName,
            Text = _text,
            Time = _time,
            Replies = _replies,
            Id = Interlocked.Increment(ref _currentId),
        };
    }
}
