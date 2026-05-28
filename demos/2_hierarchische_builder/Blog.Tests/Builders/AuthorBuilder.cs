using Blog.Models;

namespace Blog.Tests.Builders;

public class AuthorBuilder : IBuilder<Author>
{
    private static int _currentId = 0;
    private string _name = "Max Mustermann";
    private string _email = "max.mustermann@example.com";

    public AuthorBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public AuthorBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }

    public Author Build()
    {
        return new Author
        {
            Id = Interlocked.Increment(ref _currentId),
            Name = _name,
            Email = _email
        };
    }
}
