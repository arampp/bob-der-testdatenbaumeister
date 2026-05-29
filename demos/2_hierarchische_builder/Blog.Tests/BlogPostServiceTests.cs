using System;
using Blog.Models;
using Blog.Services;
using Blog.Tests.Builders;

namespace Blog.Tests;

public class BlogPostServiceTests
{
    [Fact]
    public void GetPublishedBlogPosts_DoesNotModifyCommentsOrReplies()
    {
        var publishedPost = new BlogPostBuilder()
            .WithTitle("Published Post")
            .WithIsPublished(true)
            .WithComments(c =>
                c.Add(c =>
                    c.WithText("Great post!")
                        .WithUserName("Alice")
                        .WithReplies(r =>
                            r.Add(r => r.WithContent("Thanks, Alice!").WithUserName("Bob"))
                        )
                )
            )
            .Build();

        var blogPosts = new List<BlogPost> { publishedPost };
        var blogPostService = new BlogPostService();

        // Act
        var result = blogPostService.GetPublishedPosts(blogPosts);

        // Assert
        Assert.Single(result);
        var retrievedPost = result.First();
        Assert.Single(retrievedPost.Comments);
        Assert.Equal("Great post!", retrievedPost.Comments.First().Text);
        Assert.Single(retrievedPost.Comments.First().Replies);
        Assert.Equal("Thanks, Alice!", retrievedPost.Comments.First().Replies.First().Content);
    }

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
