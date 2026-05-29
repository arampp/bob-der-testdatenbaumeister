using Blog.Models;

namespace Blog.Tests.Builders;

public class ReplyBuilder : IBuilder<Reply>
{
    private static int _currentId = 0;
    private string _content = "Thanks for the great article!";
    private string _userName = "John Doe";
    private DateTime _time = DateTime.Now;

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
            Content = _content,
            UserName = _userName,
            Time = _time
        };
    }
}
