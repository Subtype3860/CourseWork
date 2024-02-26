using BlogDAL;
using BlogDAL.Models;

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
        return GetAll();
    }

    public void DeletePostTag(PostTag postTag)
    {
        Delete(postTag);
    }

}