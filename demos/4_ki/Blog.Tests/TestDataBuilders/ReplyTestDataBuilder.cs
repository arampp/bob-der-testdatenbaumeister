using Blog.Models;

namespace Blog.Tests.TestDataBuilders;

public class ReplyTestDataBuilder
{
    private int _id = 1;
    private string _content = "Great point!";
    private string _userName = "Alice";
    private DateTime _time = new DateTime(2024, 5, 4, 10, 0, 0);

    public ReplyTestDataBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public ReplyTestDataBuilder WithContent(string content)
    {
        _content = content;
        return this;
    }

    public ReplyTestDataBuilder WithUserName(string userName)
    {
        _userName = userName;
        return this;
    }

    public ReplyTestDataBuilder WithTime(DateTime time)
    {
        _time = time;
        return this;
    }

    public Reply Build()
    {
        return new Reply
        {
            Id = _id,
            Content = _content,
            UserName = _userName,
            Time = _time,
        };
    }
}
