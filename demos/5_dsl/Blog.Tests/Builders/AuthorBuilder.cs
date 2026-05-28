using Blog.Models;
using Bogus;

namespace Blog.Tests.Builders;

public class AuthorBuilder : IBuilder<Author>
{
    private static readonly Faker Faker = new Faker("de");
    private static int _currentId = 0;
    private string? _name;
    private string? _email;

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
            Name = _name ?? Faker.Name.FullName(),
            Email = _email ?? Faker.Internet.Email(),
        };
    }
}
