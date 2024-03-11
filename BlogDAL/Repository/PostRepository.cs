using BlogDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogDAL.Repository;

public class PostRepository : Repository<Post>
{
    public PostRepository(ApplicationDbContext db) : base(db)
    {
    }
    public IEnumerable<Post> GetAllPosts()
    {
        return Set
            .Include(x => x.PostTags)!
            .ThenInclude(x => x.Tag)
            .Include(x => x.User);
    }
    public Post GetPostById(string id)
    {
        return Set
            .Include(x => x.PostTags!)
            .ThenInclude(x => x.Tag)
            .Include(x => x.User).First(x => x.Id == id);
    }
    public IEnumerable<Post> GetPostByUserId(string id)
    {
        return Set
            .Where(x => x.UserId == id)
            .Include(x => x.PostTags!)
            .ThenInclude(x => x.Tag)
            .Include(x => x.User);
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