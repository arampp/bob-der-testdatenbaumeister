using Blog.Models;

namespace Blog.Tests.TestDataBuilders;

public class AuthorTestDataBuilder
{
    private int _id = 1;
    private string _name = "John Doe";
    private string _email = "john.doe@example.com";

    public AuthorTestDataBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public AuthorTestDataBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public AuthorTestDataBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }

    public Author Build()
    {
        return new Author
        {
            Id = _id,
            Name = _name,
            Email = _email,
        };
    }
}
