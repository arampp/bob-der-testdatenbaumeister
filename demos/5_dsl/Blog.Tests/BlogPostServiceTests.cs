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
            //.WithAuthor(new AuthorBuilder().WithName("John Doe").Build())
            .WithAuthor(a => a.WithName("John Doe"))
            // .WithComments(
            // [
            //     new CommentBuilder()
            //         .WithUserName("Alice")
            //         .WithText("Great post!")
            //         .AddReply(r => r.WithUserName("John Doe").WithContent("Thank you!").WithTime(DateTime.Now))
            //         .Build()
            //     new CommentBuilder()
            //         .WithUserName("Bob")
            //         .WithText("I don't agree with everything you said.")
            //         .Build()
            // ])
            .WithComments(c =>
                c.Add(cb =>
                        cb.WithUserName("Alice")
                            .WithText("Great post!")
                            .WithReplies(r =>
                                r.Add(rb =>
                                    rb.WithUserName("John Doe")
                                        .WithContent("Thank you!")
                                        .WithTime(DateTime.Now)
                                )
                            )
                    )
                    .Add(cb =>
                        cb.WithUserName("Bob").WithText("I don't agree with everything you said.")
                    )
            )
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

    [Fact]
    public void BuildManyTest()
    {
        var posts = new BlogPostBuilder().Build(100).ToList();

        Assert.Equal(100, posts.Count);
    }
}
