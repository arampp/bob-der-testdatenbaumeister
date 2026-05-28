---
name: testdata-builder
description: "Create or extend Testdata Builders for C# entity classes. Use when: building test infrastructure, creating builder for entity, adding collection builder, setting up hierarchical builders, generating fluent API for test data. Trigger phrases: create builder, testdata builder, add builder for, extend builder with collection."
argument-hint: "Entity class name or path to create a builder for"
---

# Testdata Builder

## Why

Duplicated test data setup makes tests brittle. When an entity gains a required field, every test that instantiates it breaks. Testdata Builders centralize object creation behind a fluent API. Change propagates to one place—the builder—not to hundreds of tests.

Builders also improve readability. A test that calls `.WithIsPublished(true)` reveals intent; a 20-line object initializer obscures it.

## What

A Testdata Builder is a class that:

1. Holds private fields with realistic default values for every property of an entity.
2. Exposes `With*` methods that override one field and return `this` (method chaining).
3. Provides a `Build()` method that returns the fully constructed entity.

For entities with collections (e.g., BlogPost → Comments → Replies), use a **generic CollectionBuilder** that applies the same pattern recursively.

### Key Abstractions

| Abstraction | Purpose |
|---|---|
| `EntityBuilder` | Builds a single entity with defaults and `With*` overrides |
| `CollectionBuilder<TEntity, TBuilder>` | Builds a list of entities via repeated `Add` calls |
| `IBuilder<TEntity>` | Interface with `TEntity Build()` — enables the generic CollectionBuilder |

## How

### 1. Identify the Entity

Read the entity class. List all properties, their types, and which are `required`.

### 2. Create the Builder Class

Place it in the test project under a `TestDataBuilders/` folder (or the location already used in the project).

Follow this structure:

```csharp
public class {Entity}Builder : IBuilder<{Entity}>
{
    private Type _field = realisticDefault;

    public {Entity}Builder With{Field}(Type value)
    {
        _field = value;
        return this;
    }

    public {Entity} Build()
    {
        return new {Entity}
        {
            Field = _field
        };
    }
}
```

Rules:
- Class name: `{EntityName}Builder`
- Every `With*` method returns `this`
- Default values are realistic (e.g., "John Doe", not "string1")
- Dependencies on other entities use their builders: `private Author _author = new AuthorBuilder().Build();`
- IDs start at 1; dates use `DateTime.Now.AddDays(-1)` or similar relative values

### 3. Add Collection Support (if needed)

When the entity owns a collection property, integrate the generic `CollectionBuilder`:

```csharp
public {Parent}Builder With{Children}(
    Func<CollectionBuilder<{Child}, {Child}Builder>,
         CollectionBuilder<{Child}, {Child}Builder>> configure)
{
    var builder = new CollectionBuilder<{Child}, {Child}Builder>();
    var configuredBuilder = configure(builder);
    _{children} = configuredBuilder.Build();
    return this;
}
```

If `CollectionBuilder<TEntity, TBuilder>` does not yet exist in the project, create it:

```csharp
public class CollectionBuilder<TEntity, TBuilder> : IBuilder<IEnumerable<TEntity>>
    where TBuilder : IBuilder<TEntity>, new()
{
    private readonly List<TEntity> _items = new();

    public CollectionBuilder<TEntity, TBuilder> Add(Action<TBuilder>? configure = null)
    {
        var builder = new TBuilder();
        configure?.Invoke(builder);
        _items.Add(builder.Build());
        return this;
    }

    public IEnumerable<TEntity> Build() => _items;
}
```

### 4. Add Convenience Methods (optional)

For common test scenarios, add named methods that configure multiple fields at once:

```csharp
public BlogPostBuilder WithDraftPost()
{
    _isPublished = false;
    _publishedDate = DateTime.Now.AddDays(1);
    return this;
}
```

### 5. Verify

- Builder compiles
- `Build()` returns a valid entity with all required properties set
- Nested builders compose without circular dependencies

## What If

- **Deep hierarchies**: Apply the pattern recursively. Each level uses its own builder and collection builder. The API stays consistent across all levels.
- **Entity gains a new required property**: Add the field with a default to the builder. No existing tests break.
- **Multiple test scenarios need the same configuration**: Extract a convenience method (`WithDraftPost`, `WithPublishedPost`).
- **Builder already exists but lacks a property**: Add the missing `With*` method and a sensible default.
- **Generic CollectionBuilder already exists**: Reuse it. Do not create entity-specific collection builders.
