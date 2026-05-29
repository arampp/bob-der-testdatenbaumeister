using Blog.Models;

namespace Blog.Tests.TestDataBuilders;

public class CommentTestDataBuilder
{
    private int _id = 1;
    private string _userName = "John Smith";
    private string _text = "Excellent article!";
    private DateTime _time = new DateTime(2024, 5, 3, 14, 30, 0);
    private IEnumerable<Reply> _replies = [];

    public CommentTestDataBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public CommentTestDataBuilder WithUserName(string userName)
    {
        _userName = userName;
        return this;
    }

    public CommentTestDataBuilder WithText(string text)
    {
        _text = text;
        return this;
    }

    public CommentTestDataBuilder WithTime(DateTime time)
    {
        _time = time;
        return this;
    }

    public CommentTestDataBuilder WithReplies(IEnumerable<Reply> replies)
    {
        _replies = replies;
        return this;
    }

    public CommentTestDataBuilder WithReplies(params Reply[] replies)
    {
        _replies = replies;
        return this;
    }

    public Comment Build()
    {
        return new Comment
        {
            Id = _id,
            UserName = _userName,
            Text = _text,
            Time = _time,
            Replies = _replies,
        };
    }
}
