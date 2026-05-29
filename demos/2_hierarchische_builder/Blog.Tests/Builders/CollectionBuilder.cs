namespace Blog.Tests.Builders;

public class CollectionBuilder<TBuilder, TEntity>
    where TBuilder : IBuilder<TEntity>, new()
{
    private readonly List<TEntity> _items = [];

    public CollectionBuilder<TBuilder, TEntity> Add(Action<TBuilder> configure)
    {
        var builder = new TBuilder();
        configure(builder);
        _items.Add(builder.Build());
        return this;
    }

    public IEnumerable<TEntity> Build()
    {
        return _items;
    }
}
