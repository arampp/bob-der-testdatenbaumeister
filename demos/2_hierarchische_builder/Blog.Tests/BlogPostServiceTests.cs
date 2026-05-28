using System;
using Blog.Models;
using Blog.Services;
using Blog.Tests.Builders;

namespace Blog.Tests;

public class BlogPostServiceTests
{
    [Fact]
    public void GetPublishedBlogPosts_ReturnsOnlyPublishedPosts()
    {
        // Arrange
        var publishedPost = new BlogPostBuilder()
            .WithTitle("Published Post")
            .WithIsPublished(true)
            .Build();
        var unpublishedPost = new BlogPostBuilder()
            .WithTitle("Unpublished Post")
            .WithIsPublished(false)
            .Build();

        var blogPosts = new List<BlogPost> { publishedPost, unpublishedPost };
        var blogPostService = new BlogPostService();

        // Act
        var result = blogPostService.GetPublishedPosts(blogPosts);

        // Assert
        Assert.Single(result);
        Assert.Equal("Published Post", result.First().Title);
    }
}
