namespace Blog.Tests.Builders;

public static class Create
{
    public static BlogPostBuilder DraftPost()
    {
        return new BlogPostBuilder().WithIsPublished(false).WithPublishedDate(null);
    }

    public static BlogPostBuilder PublishedPost()
    {
        return new BlogPostBuilder().WithIsPublished(true).WithPublishedDate(DateTime.Now);
    }

    public static BlogPostBuilder ScheduledPost()
    {
        return new BlogPostBuilder().WithIsPublished(false).WithPublishedDate(DateTime.Now.AddDays(7));
    }
}