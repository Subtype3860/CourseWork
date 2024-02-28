using BlogDAL;
using BlogDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogBLL.Repository;

public class RemarkRepository : Repository<Remark>
{
    public RemarkRepository(ApplicationDbContext db) : base(db)
    {
    }

    public IEnumerable<Remark> GetAllComments()
    {
        return Set
            .Include(x=>x.Post)
            .Include(x=>x.User);
    }

    public IEnumerable<Remark> GetCommentByPostId(string id)
    {
        return Set
            .Include(x=>x.User)
            .Include(x=>x.Post)
            .Where(x => x.PostId == id);
    }

    public Remark GetRemarkById(string id)
    {
        return Set
            .Include(x => x.Post)
            .Include(x => x.User)
            .First(x => x.Id == id);
    }

    public void AddComment(Remark comment)
    {
        Create(comment);
    }

    public void UpdateComment(Remark comment)
    {
        Update(comment);
    }

    public void RemoveComment(Remark comment)
    {
        Delete(comment);
    }
}