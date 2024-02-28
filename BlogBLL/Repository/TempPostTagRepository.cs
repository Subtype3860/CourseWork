using BlogBLL.ViewModels.Post;
using BlogDAL;
using BlogDAL.Models;

namespace BlogBLL.Repository;

public class TempPostTagRepository : Repository<TempPostTag>
{
    public TempPostTagRepository(ApplicationDbContext db) : base(db)
    {
    }

    public IEnumerable<TempPostTag> GetTempPostTeg()
    {
        return Set.AsEnumerable();
    }
}