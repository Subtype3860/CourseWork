using BlogDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogDAL.Repository;

public class TagRepository : Repository<Tag>
{
    public TagRepository(ApplicationDbContext db) : base(db)
    {
    }

    public IEnumerable<Tag> GetAllTags()
    {
        return Set
            .Include(x=>x.PostTags);
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

    public Tag GetTagByName(string name)
    {
        var tag = Set.FirstOrDefault(x => x.Stick == name);
        return tag!;
    }
}