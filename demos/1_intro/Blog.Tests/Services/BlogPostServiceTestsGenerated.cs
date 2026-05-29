using Blog.Models;
using Blog.Services;

namespace Blog.Tests.Builders;

public class BlogPostServiceTestsGenerated
{
    private static Author CreateTestAuthor(
        int id,
        string name = "Test Author",
        string email = "test@example.com"
    )
    {
        return new Author
        {
            Id = id,
            Name = name,
            Email = email,
        };
    }

    private static Category CreateTestCategory(int id, string name = "Test Category")
    {
        return new Category
        {
            Id = id,
            Name = name,
            Description = "A test category",
        };
    }

    private static BlogPost CreateTestPost(
        int id,
        string title,
        string content,
        Author author,
        bool isPublished,
        Category? category = null,
        DateTime? publishedDate = null
    )
    {
        return new BlogPost
        {
            Id = id,
            Title = title,
            Content = content,
            Author = author,
            IsPublished = isPublished,
            Category = category,
            PublishedDate = publishedDate,
        };
    }

    [Fact]
    public void GetPublishedPosts_WithMixedPublishedAndUnpublishedPosts_ReturnsOnlyPublishedPosts()
    {
        // Arrange
        var author = CreateTestAuthor(1, "Alice Johnson", "alice.johnson@blog.com");
        var techCategory = CreateTestCategory(1, "Technology");
        var businessCategory = CreateTestCategory(2, "Business");

        var posts = new List<BlogPost>
        {
            CreateTestPost(
                1,
                "Getting Started with C#",
                "A comprehensive guide to C# fundamentals...",
                author,
                isPublished: true,
                category: techCategory,
                publishedDate: new DateTime(2024, 1, 15)
            ),
            CreateTestPost(
                2,
                "Work in Progress: Advanced LINQ",
                "Draft: Advanced LINQ patterns...",
                author,
                isPublished: false,
                category: techCategory
            ),
            CreateTestPost(
                3,
                "Digital Transformation Trends 2024",
                "Key trends in digital transformation...",
                author,
                isPublished: true,
                category: businessCategory,
                publishedDate: new DateTime(2024, 2, 20)
            ),
            CreateTestPost(
                4,
                "Draft: Machine Learning Basics",
                "Introduction to ML algorithms...",
                author,
                isPublished: false,
                category: techCategory
            ),
            CreateTestPost(
                5,
                "Leadership Tips for Managers",
                "Essential leadership practices...",
                author,
                isPublished: true,
                category: businessCategory,
                publishedDate: new DateTime(2024, 3, 10)
            ),
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
        var author1 = CreateTestAuthor(1, "Bob Smith", "bob.smith@blog.com");
        var author2 = CreateTestAuthor(2, "Carol White", "carol.white@blog.com");
        var techCategory = CreateTestCategory(1, "Technology");

        var posts = new List<BlogPost>
        {
            CreateTestPost(
                1,
                "Introduction to REST APIs",
                "Learn the basics of REST API design...",
                author1,
                isPublished: true,
                category: techCategory,
                publishedDate: new DateTime(2024, 1, 5)
            ),
            CreateTestPost(
                2,
                "Docker Containerization Guide",
                "Complete guide to Docker...",
                author2,
                isPublished: true,
                category: techCategory,
                publishedDate: new DateTime(2024, 1, 12)
            ),
            CreateTestPost(
                3,
                "Kubernetes Orchestration",
                "Mastering Kubernetes...",
                author1,
                isPublished: true,
                category: techCategory,
                publishedDate: new DateTime(2024, 1, 20)
            ),
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
        var author = CreateTestAuthor(1, "David Brown", "david.brown@blog.com");
        var draftCategory = CreateTestCategory(3, "Drafts");

        var posts = new List<BlogPost>
        {
            CreateTestPost(
                1,
                "Draft: New Framework Review",
                "Waiting for more research...",
                author,
                isPublished: false,
                category: draftCategory
            ),
            CreateTestPost(
                2,
                "Work in Progress: Security Best Practices",
                "Still gathering information...",
                author,
                isPublished: false,
                category: draftCategory
            ),
            CreateTestPost(
                3,
                "Idea: Performance Optimization",
                "Initial concept...",
                author,
                isPublished: false
            ),
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
        var author = CreateTestAuthor(1, "Emma Davis", "emma.davis@blog.com");
        var lifestyleCategory = CreateTestCategory(4, "Lifestyle");

        var publishedPost = CreateTestPost(
            1,
            "The Art of Work-Life Balance",
            "Strategies for maintaining balance in today's fast-paced world...",
            author,
            isPublished: true,
            category: lifestyleCategory,
            publishedDate: new DateTime(2024, 2, 28)
        );

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
        var author = CreateTestAuthor(1, "Frank Miller", "frank.miller@blog.com");
        var techCategory = CreateTestCategory(1, "Technology");

        var posts = new List<BlogPost>
        {
            CreateTestPost(
                1,
                "First Article",
                "Content...",
                author,
                isPublished: true,
                category: techCategory
            ),
            CreateTestPost(2, "Unpublished Article", "Content...", author, isPublished: false),
            CreateTestPost(
                3,
                "Second Article",
                "Content...",
                author,
                isPublished: true,
                category: techCategory
            ),
            CreateTestPost(
                4,
                "Third Article",
                "Content...",
                author,
                isPublished: true,
                category: techCategory
            ),
        };

        var service = new BlogPostService();

        // Act
        var publishedPosts = service.GetPublishedPosts(posts).ToList();

        // Assert
        Assert.Equal(3, publishedPosts.Count);
        Assert.Equal(1, publishedPosts[0].Id);
        Assert.Equal(3, publishedPosts[1].Id);
        Assert.Equal(4, publishedPosts[2].Id);
    }

    [Fact]
    public void GetPublishedPosts_WithPostsWithoutCategory_ReturnsPublishedPosts()
    {
        // Arrange
        var author = CreateTestAuthor(1, "Grace Lee", "grace.lee@blog.com");

        var posts = new List<BlogPost>
        {
            CreateTestPost(
                1,
                "Article Without Category",
                "This article has no category...",
                author,
                isPublished: true,
                publishedDate: new DateTime(2024, 3, 1)
            ),
            CreateTestPost(
                2,
                "Unpublished: Article With Category",
                "This article is unpublished...",
                author,
                isPublished: false,
                category: CreateTestCategory(1, "Technology")
            ),
            CreateTestPost(
                3,
                "Another Article Without Category",
                "Also no category...",
                author,
                isPublished: true,
                publishedDate: new DateTime(2024, 3, 5)
            ),
        };

        var service = new BlogPostService();

        // Act
        var publishedPosts = service.GetPublishedPosts(posts).ToList();

        // Assert
        Assert.Equal(2, publishedPosts.Count);
        Assert.All(publishedPosts, post => Assert.True(post.IsPublished));
    }
}
