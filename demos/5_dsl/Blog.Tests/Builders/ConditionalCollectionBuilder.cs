namespace Blog.Tests.Builders;

public class ConditionalCollectionBuilder<TEntity, TBuilder>
    where TBuilder : IBuilder<TEntity>, new()
{
    private readonly List<TEntity> _items = new();
    private int _currentCount = 0;

    public ConditionalCollectionBuilder<TEntity, TBuilder> Where(int count)
    {
        _currentCount = count;
        return this;
    }

    public ConditionalCollectionBuilder<TEntity, TBuilder> And(int count)
    {
        return Where(count);
    }

    public ConditionalCollectionBuilder<TEntity, TBuilder> Are(Action<TBuilder> configure)
    {
        for (int i = 0; i < _currentCount; i++)
        {
            var builder = new TBuilder();
            configure(builder);
            _items.Add(builder.Build());
        }
        _currentCount = 0;
        return this;
    }

    public IEnumerable<TEntity> Build()
    {
        return _items;
    }

    public static implicit operator List<TEntity>(ConditionalCollectionBuilder<TEntity, TBuilder> builder)
    {
        return builder.Build().ToList();
    }
}