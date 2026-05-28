
using Blog.Models;

namespace Blog.Tests.Builders;

public class CategoryBuilder : IBuilder<Category>
{
    private static int _currentId = 0;
    private string _name = "Technology";
    private string _description = "Articles about technology and programming";

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
            Name = _name,
            Description = _description
        };
    }
}
