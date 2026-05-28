namespace Blog.Tests.Builders;

public class CollectionBuilder<TBuilder, T> : IBuilder<IEnumerable<T>>
    where TBuilder : IBuilder<T>, new()
{
    private readonly List<T> _items = [];

    public CollectionBuilder<TBuilder, T> Add(Action<TBuilder> configure)
    {
        var builder = new TBuilder();
        configure(builder);
        _items.Add(builder.Build());
        return this;
    }

    public IEnumerable<T> Build() => _items;
}
