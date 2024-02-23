using BlogDAL;
using BlogDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogBLL.Repository;

public class PostRepository : Repository<Post>
{
    public PostRepository(ApplicationDbContext db) : base(db)
    {
    }

    public IEnumerable<Post> GetAllPosts()
    {
        return Set.Include(x => x.PostTags)!
            .ThenInclude(x => x.Tag);

    }

    public IEnumerable<Post> GetPostByTag(Tag tag)
    {
        return Set
            .Include(x => x.PostTags!.Where(w => w.TagId == tag.Id))
            .ThenInclude(x=>x.Tag);
    }

    public void AddPost(string? body, User user)
    {
        var model = new Post
        {
            Body = body,
            UserId = user.Id
        };
        Create(model);
    }

    public void PostUpdate(Post post)
    {
        Update(post);
    }

    public void PostRemove(Post post)
    {
        Delete(post);
    }
}