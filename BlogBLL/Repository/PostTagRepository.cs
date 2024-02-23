using BlogDAL;
using BlogDAL.Models;

namespace BlogBLL.Repository;

public class PostTagRepository : Repository<PostTag>
{
    public PostTagRepository(ApplicationDbContext db) : base(db)
    {
    }

    public void AddPostTag(string? PostId, string? TagId)
    {
        
    }

    public IEnumerable<PostTag> GetAllPostTags()
    {
        return GetAll();
    }

    public void DeletePostTag()
    {
        
    }
}