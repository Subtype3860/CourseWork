using BlogDAL;
using BlogDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogBLL.Repository;

public class PostTagRepository : Repository<PostTag>
{
    public PostTagRepository(ApplicationDbContext db) : base(db)
    {
    }
    public void AddPostTag(PostTag postTag)
    {
        Create(postTag);
    }
    public IEnumerable<PostTag> GetAllPostTags()
    {
        return Set
            .Include(x=>x.Post)
            .Include(x=>x.Tag);
    }
    public void DeletePostTag(PostTag postTag)
    {
        Delete(postTag);
    }

}