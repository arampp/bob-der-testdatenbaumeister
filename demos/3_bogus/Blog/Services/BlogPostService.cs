using Blog.Models;

namespace Blog.Services;

public class BlogPostService
{
    public IEnumerable<BlogPost> GetPublishedPosts(IEnumerable<BlogPost> posts)
    {
        return posts.Where(post => post.IsPublished);
    }

    public IEnumerable<BlogPost> GetPostsPage(
        IEnumerable<BlogPost> posts,
        int pageNumber,
        int pageSize)
    {
        if (pageNumber <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(pageNumber));
        }

        if (pageSize <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(pageSize));
        }

        return posts
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }
}
