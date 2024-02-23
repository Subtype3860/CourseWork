using BlogDAL;
using BlogDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogBLL.Repository;

public class TagRepository : Repository<Tag>
{
    public TagRepository(ApplicationDbContext db) : base(db)
    {
    }

    public IEnumerable<Tag> GetAllTags()
    {
        return Set;
    }

    public string AddTag(Tag tag)
    {
        var model = new Tag
        {
            Stick = tag.Stick
        };
        Create(model);
        return model.Id!;
    }

    public void UpdateTag(Tag tag)
    {
        Update(tag);
    }

    public void TagRemove(Tag tag)
    {
        Delete(tag);
    }
}