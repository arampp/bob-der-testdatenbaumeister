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
        var publishedPost = Create.PublishedPost().WithTitle("Published Post");
        var unpublishedPost = Create.DraftPost();

        var blogPosts = new List<BlogPost> { publishedPost, unpublishedPost };
        var blogPostService = new BlogPostService();

        // Act
        var result = blogPostService.GetPublishedPosts(blogPosts);

        // Assert
        Assert.Single(result);
        Assert.Equal("Published Post", result.First().Title);
    }

    [Fact]
    public void BuildManyTest()
    {
        var posts = new BlogPostBuilder().Build(100).ToList();

        Assert.Equal(100, posts.Count);
    }
}
