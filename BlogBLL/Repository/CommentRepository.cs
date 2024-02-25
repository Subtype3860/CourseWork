using BlogDAL;
using BlogDAL.Models;

namespace BlogBLL.Repository;

public class CommentRepository : Repository<Remark>
{
    public CommentRepository(ApplicationDbContext db) : base(db)
    {
    }

    public IEnumerable<Remark> GetAllComments()
    {
        return GetAll();
    }

    public IEnumerable<Remark> GetCommentByPost(string id)
    {
        return GetAll().Where(x => x.PostId == id);
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