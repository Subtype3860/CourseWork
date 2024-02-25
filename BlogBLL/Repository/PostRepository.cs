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
        return Set;

    }

    public IEnumerable<Post> GetPostByTag(Tag tag)
    {
        return Set
            .Include(x => x.PostTags!.Where(w => w.TagId == tag.Id));
    }

    public void AddPost(Post post)
    {
        Create(post);
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