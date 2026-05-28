namespace Blog.Tests.Builders;

public static class BlogPostBuilderExtensions
{
    public static BlogPostBuilder InCategory(this BlogPostBuilder builder, string categoryName)
    {
        return builder.WithCategory(new CategoryBuilder().WithName(categoryName).Build());
    }

    public static BlogPostBuilder FromLastWeek(this BlogPostBuilder builder)
    {
        return builder.WithPublishedDate(DateTime.Now.AddDays(-7));
    }

    public static BlogPostBuilder FromYesterday(this BlogPostBuilder builder)
    {
        return builder.WithPublishedDate(DateTime.Now.AddDays(-1));
    }

    public static BlogPostBuilder Published(this BlogPostBuilder builder)
    {
        return builder.WithIsPublished(true).WithPublishedDate(DateTime.Now);
    }
    public static BlogPostBuilder Draft(this BlogPostBuilder builder)
    {
        return builder.WithIsPublished(false).WithPublishedDate(null);
    }
}