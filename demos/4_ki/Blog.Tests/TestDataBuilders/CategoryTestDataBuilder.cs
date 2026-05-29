using Blog.Models;

namespace Blog.Tests.TestDataBuilders;

public class CategoryTestDataBuilder
{
    private int _id = 1;
    private string _name = "Technology";
    private string _description = "Technology and software development articles";

    public CategoryTestDataBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public CategoryTestDataBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public CategoryTestDataBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public Category Build()
    {
        return new Category
        {
            Id = _id,
            Name = _name,
            Description = _description,
        };
    }
}
