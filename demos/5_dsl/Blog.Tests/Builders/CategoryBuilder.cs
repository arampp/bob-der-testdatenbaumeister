using Blog.Models;
using Bogus;

namespace Blog.Tests.Builders;

public class CategoryBuilder : IBuilder<Category>
{
    private static readonly Faker Faker = new Faker("de");
    private static int _currentId = 0;
    private string? _name;
    private string? _description;

    public CategoryBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public CategoryBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public Category Build()
    {
        return new Category
        {
            Id = Interlocked.Increment(ref _currentId),
            Name = _name ?? Faker.Music.Genre(),
            Description = _description ?? Faker.Lorem.Sentence(),
        };
    }
}
