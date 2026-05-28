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
        var blogPosts = Create.BlogPosts()
            .Where(5).Are(p => p.Published().WithTitle("Published Post"))
            .And(3).Are(p => p.Draft().WithTitle("Unpublished Post"))
            .Build();
        var blogPostService = new BlogPostService();

        // Act
        var result = blogPostService.GetPublishedPosts(blogPosts);

        // Assert
        Assert.All(result, post => Assert.True(post.IsPublished));
        Assert.Equal(5, result.Count());
    }

    [Fact]
    public void BuildManyTest()
    {
        var posts = new BlogPostBuilder().Build(100).ToList();

        Assert.Equal(100, posts.Count);
    }
}
