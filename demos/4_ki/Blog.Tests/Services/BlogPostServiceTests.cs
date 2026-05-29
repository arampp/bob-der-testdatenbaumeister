using Blog.Models;
using Blog.Services;
using Blog.Tests.TestDataBuilders;

namespace Blog.Tests.Services;

public class BlogPostServiceTests
{
    private readonly BlogPostService _service = new();

    #region GetPublishedPosts Tests

    [Fact]
    public void GetPublishedPosts_EmptyList_ReturnsEmptyEnumerable()
    {
        // Arrange
        var posts = new List<BlogPost>();

        // Act
        var result = _service.GetPublishedPosts(posts);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GetPublishedPosts_AllPostsPublished_ReturnsAllPosts()
    {
        // Arrange
        var publishedPost1 = new BlogPostTestDataBuilder()
            .WithId(1)
            .WithTitle("First Article")
            .WithIsPublished(true)
            .Build();

        var publishedPost2 = new BlogPostTestDataBuilder()
            .WithId(2)
            .WithTitle("Second Article")
            .WithIsPublished(true)
            .Build();

        var publishedPost3 = new BlogPostTestDataBuilder()
            .WithId(3)
            .WithTitle("Third Article")
            .WithIsPublished(true)
            .Build();

        var posts = new List<BlogPost> { publishedPost1, publishedPost2, publishedPost3 };

        // Act
        var result = _service.GetPublishedPosts(posts).ToList();

        // Assert
        Assert.Equal(3, result.Count);
        Assert.All(result, post => Assert.True(post.IsPublished));
        Assert.Contains(publishedPost1, result);
        Assert.Contains(publishedPost2, result);
        Assert.Contains(publishedPost3, result);
    }

    [Fact]
    public void GetPublishedPosts_AllPostsUnpublished_ReturnsEmptyEnumerable()
    {
        // Arrange
        var unpublishedPost1 = new BlogPostTestDataBuilder()
            .WithId(1)
            .WithTitle("Draft Article 1")
            .WithIsPublished(false)
            .Build();

        var unpublishedPost2 = new BlogPostTestDataBuilder()
            .WithId(2)
            .WithTitle("Draft Article 2")
            .WithIsPublished(false)
            .Build();

        var unpublishedPost3 = new BlogPostTestDataBuilder()
            .WithId(3)
            .WithTitle("Draft Article 3")
            .WithIsPublished(false)
            .Build();

        var posts = new List<BlogPost> { unpublishedPost1, unpublishedPost2, unpublishedPost3 };

        // Act
        var result = _service.GetPublishedPosts(posts);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GetPublishedPosts_MixedPublishedAndUnpublished_ReturnsOnlyPublishedPosts()
    {
        // Arrange
        var publishedPost1 = new BlogPostTestDataBuilder()
            .WithId(1)
            .WithTitle("Published Article 1")
            .WithIsPublished(true)
            .Build();

        var unpublishedPost1 = new BlogPostTestDataBuilder()
            .WithId(2)
            .WithTitle("Draft Article 1")
            .WithIsPublished(false)
            .Build();

        var publishedPost2 = new BlogPostTestDataBuilder()
            .WithId(3)
            .WithTitle("Published Article 2")
            .WithIsPublished(true)
            .Build();

        var unpublishedPost2 = new BlogPostTestDataBuilder()
            .WithId(4)
            .WithTitle("Draft Article 2")
            .WithIsPublished(false)
            .Build();

        var posts = new List<BlogPost>
        {
            publishedPost1,
            unpublishedPost1,
            publishedPost2,
            unpublishedPost2,
        };

        // Act
        var result = _service.GetPublishedPosts(posts).ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.All(result, post => Assert.True(post.IsPublished));
        Assert.Contains(publishedPost1, result);
        Assert.Contains(publishedPost2, result);
        Assert.DoesNotContain(unpublishedPost1, result);
        Assert.DoesNotContain(unpublishedPost2, result);
    }

    [Fact]
    public void GetPublishedPosts_SinglePublishedPost_ReturnsThatPost()
    {
        // Arrange
        var publishedPost = new BlogPostTestDataBuilder()
            .WithId(1)
            .WithTitle("Single Published Post")
            .WithContent("This is the only published post")
            .WithAuthor(
                new AuthorTestDataBuilder()
                    .WithName("Jane Smith")
                    .WithEmail("jane.smith@example.com")
            )
            .WithCategory(
                new CategoryTestDataBuilder()
                    .WithName("News")
                    .WithDescription("Latest news and updates")
            )
            .WithIsPublished(true)
            .Build();

        var posts = new List<BlogPost> { publishedPost };

        // Act
        var result = _service.GetPublishedPosts(posts).ToList();

        // Assert
        Assert.Single(result);
        Assert.Equal(publishedPost, result[0]);
        Assert.True(result[0].IsPublished);
    }

    [Fact]
    public void GetPublishedPosts_SingleUnpublishedPost_ReturnsEmptyEnumerable()
    {
        // Arrange
        var unpublishedPost = new BlogPostTestDataBuilder()
            .WithId(1)
            .WithTitle("Single Unpublished Post")
            .WithContent("This draft post is not published yet")
            .WithIsPublished(false)
            .Build();

        var posts = new List<BlogPost> { unpublishedPost };

        // Act
        var result = _service.GetPublishedPosts(posts);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GetPublishedPosts_PostsWithCommentsAndReplies_PreservesAllData()
    {
        // Arrange
        var reply = new ReplyTestDataBuilder().WithContent("I agree!").WithUserName("Bob").Build();

        var comment = new CommentTestDataBuilder()
            .WithUserName("Alice")
            .WithText("Great article!")
            .WithReplies(reply)
            .Build();

        var publishedPost = new BlogPostTestDataBuilder()
            .WithId(1)
            .WithTitle("Article with Comments")
            .WithIsPublished(true)
            .WithComments(comment)
            .Build();

        var posts = new List<BlogPost> { publishedPost };

        // Act
        var result = _service.GetPublishedPosts(posts).ToList();

        // Assert
        Assert.Single(result);
        var resultPost = result[0];
        Assert.Single(resultPost.Comments);
        var resultComment = resultPost.Comments.First();
        Assert.Equal("Alice", resultComment.UserName);
        Assert.Single(resultComment.Replies);
    }

    [Fact]
    public void GetPublishedPosts_PostsWithAndWithoutCategories_ReturnsOnlyPublishedRegardlessOfCategory()
    {
        // Arrange
        var publishedPostWithCategory = new BlogPostTestDataBuilder()
            .WithId(1)
            .WithTitle("Published with Category")
            .WithIsPublished(true)
            .WithCategory(new CategoryTestDataBuilder())
            .Build();

        var publishedPostWithoutCategory = new BlogPostTestDataBuilder()
            .WithId(2)
            .WithTitle("Published without Category")
            .WithIsPublished(true)
            .WithoutCategory()
            .Build();

        var unpublishedPostWithCategory = new BlogPostTestDataBuilder()
            .WithId(3)
            .WithTitle("Unpublished with Category")
            .WithIsPublished(false)
            .WithCategory(new CategoryTestDataBuilder())
            .Build();

        var posts = new List<BlogPost>
        {
            publishedPostWithCategory,
            publishedPostWithoutCategory,
            unpublishedPostWithCategory,
        };

        // Act
        var result = _service.GetPublishedPosts(posts).ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.All(result, post => Assert.True(post.IsPublished));
    }

    [Fact]
    public void GetPublishedPosts_LargeCollectionWithManyUnpublished_ReturnsOnlyPublishedInCorrectOrder()
    {
        // Arrange
        var posts = new List<BlogPost>();
        for (int i = 1; i <= 100; i++)
        {
            var post = new BlogPostTestDataBuilder()
                .WithId(i)
                .WithTitle($"Article {i}")
                .WithIsPublished(i % 2 == 0) // Even numbers are published
                .Build();
            posts.Add(post);
        }

        // Act
        var result = _service.GetPublishedPosts(posts).ToList();

        // Assert
        Assert.Equal(50, result.Count);
        Assert.All(result, post => Assert.True(post.IsPublished));
        // Verify order is preserved
        for (int i = 0; i < result.Count; i++)
        {
            Assert.Equal((i + 1) * 2, result[i].Id);
        }
    }

    #endregion
}
