using Blog.Models;
using Blog.Services;

namespace Blog.Tests.Builders;

public class BlogPostServiceTestsWithTestdataBuilder
{
    [Fact]
    public void GetPublishedPosts_WithMixedPublishedAndUnpublishedPosts_ReturnsOnlyPublishedPosts()
    {
        // Arrange
        var posts = new List<BlogPost>
        {
            new BlogPostBuilder()
                .WithTitle("Getting Started with C#")
                .WithIsPublished(true)
                .Build(),
            new BlogPostBuilder()
                .WithTitle("Work in Progress: Advanced LINQ")
                .WithIsPublished(false)
                .Build(),
            new BlogPostBuilder()
                .WithTitle("Digital Transformation Trends 2024")
                .WithIsPublished(true)
                .Build(),
            new BlogPostBuilder()
                .WithTitle("Draft: Machine Learning Basics")
                .WithIsPublished(false)
                .Build(),
            new BlogPostBuilder()
                .WithTitle("Leadership Tips for Managers")
                .WithIsPublished(true)
                .Build(),
        };

        var service = new BlogPostService();

        // Act
        var publishedPosts = service.GetPublishedPosts(posts).ToList();

        // Assert
        Assert.Equal(3, publishedPosts.Count);
        Assert.All(publishedPosts, post => Assert.True(post.IsPublished));
        Assert.Contains(publishedPosts, p => p.Title == "Getting Started with C#");
        Assert.Contains(publishedPosts, p => p.Title == "Digital Transformation Trends 2024");
        Assert.Contains(publishedPosts, p => p.Title == "Leadership Tips for Managers");
    }

    [Fact]
    public void GetPublishedPosts_WithEmptyPostsList_ReturnsEmptyList()
    {
        // Arrange
        var posts = new List<BlogPost>();
        var service = new BlogPostService();

        // Act
        var publishedPosts = service.GetPublishedPosts(posts).ToList();

        // Assert
        Assert.Empty(publishedPosts);
    }

    [Fact]
    public void GetPublishedPosts_WithAllPublishedPosts_ReturnsAllPosts()
    {
        // Arrange
        var posts = new List<BlogPost>
        {
            new BlogPostBuilder().WithIsPublished(true).Build(),
            new BlogPostBuilder().WithIsPublished(true).Build(),
            new BlogPostBuilder().WithIsPublished(true).Build(),
        };

        var service = new BlogPostService();

        // Act
        var publishedPosts = service.GetPublishedPosts(posts).ToList();

        // Assert
        Assert.Equal(3, publishedPosts.Count);
        Assert.Equal(posts.Count, publishedPosts.Count);
    }

    [Fact]
    public void GetPublishedPosts_WithNoPublishedPosts_ReturnsEmptyList()
    {
        // Arrange
        var posts = new List<BlogPost>
        {
            new BlogPostBuilder().WithIsPublished(false).Build(),
            new BlogPostBuilder().WithIsPublished(false).Build(),
            new BlogPostBuilder().WithIsPublished(false).Build(),
        };

        var service = new BlogPostService();

        // Act
        var publishedPosts = service.GetPublishedPosts(posts).ToList();

        // Assert
        Assert.Empty(publishedPosts);
    }

    [Fact]
    public void GetPublishedPosts_WithSinglePublishedPost_ReturnsThatPost()
    {
        // Arrange
        var publishedPost = new BlogPostBuilder().WithIsPublished(true).Build();

        var posts = new List<BlogPost> { publishedPost };
        var service = new BlogPostService();

        // Act
        var publishedPosts = service.GetPublishedPosts(posts).ToList();

        // Assert
        Assert.Single(publishedPosts);
        Assert.Equal(publishedPost.Id, publishedPosts[0].Id);
        Assert.Equal(publishedPost.Title, publishedPosts[0].Title);
    }

    [Fact]
    public void GetPublishedPosts_PreservesPostOrder()
    {
        // Arrange
        var posts = new List<BlogPost>
        {
            new BlogPostBuilder().WithIsPublished(true).Build(),
            new BlogPostBuilder().WithIsPublished(false).Build(),
            new BlogPostBuilder().WithIsPublished(true).Build(),
            new BlogPostBuilder().WithIsPublished(true).Build(),
        };

        var service = new BlogPostService();

        // Act
        var publishedPosts = service.GetPublishedPosts(posts).ToList();

        // Assert
        Assert.Equal(3, publishedPosts.Count);
        Assert.Equal(posts[0].Id, publishedPosts[0].Id);
        Assert.Equal(posts[2].Id, publishedPosts[1].Id);
        Assert.Equal(posts[3].Id, publishedPosts[2].Id);
    }

    [Fact]
    public void GetPublishedPosts_WithPostsWithoutCategory_ReturnsPublishedPosts()
    {
        // Arrange
        var posts = new List<BlogPost>
        {
            new BlogPostBuilder().WithIsPublished(true).Build(),
            new BlogPostBuilder()
                .WithIsPublished(false)
                .WithCategory(new Category { Id = 1, Name = "Technology" })
                .Build(),
            new BlogPostBuilder().WithIsPublished(true).Build(),
        };

        var service = new BlogPostService();

        // Act
        var publishedPosts = service.GetPublishedPosts(posts).ToList();

        // Assert
        Assert.Equal(2, publishedPosts.Count);
        Assert.All(publishedPosts, post => Assert.True(post.IsPublished));
    }
}
