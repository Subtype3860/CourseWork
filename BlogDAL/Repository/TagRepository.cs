using BlogDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogDAL.Repository;

public class TagRepository : Repository<Tag>
{
    public TagRepository(AppDbContext db) : base(db)
    {
    }

    public IEnumerable<Tag> GetAllTags()
    {
        return Set
            .Include(x=>x.PostTags);
    }

    public IEnumerable<Tag> GetAllTagsByPostId(string id)
    {
        return Set
            .Include(x => x.PostTags.Where(w => w.PostId == id));
    }

    public void AddTag(Tag tag)
    {
        Create(tag);
    }

    public void UpdateTag(Tag tag)
    {
        Update(tag);
    }

    public void TagRemove(Tag tag)
    {
        Delete(tag);
    }

    public Tag GetTagById(string id)
    {
        return Get(id);
    }
}