using BlogDAL;
using BlogDAL.Models;

namespace BlogBLL.Repository;

public class CommentRepository : Repository<Commentaries>
{
    public CommentRepository(ApplicationDbContext db) : base(db)
    {
    }

    public IEnumerable<Commentaries> GetAllComments()
    {
        return GetAll();
    }

    public IEnumerable<Commentaries> GetCommentByPost(string id)
    {
        return GetAll().Where(x => x.PostId == id);
    }

    public void AddComment(Commentaries comment)
    {
        Create(comment);
    }

    public void UpdateComment(Commentaries comment)
    {
        Update(comment);
    }

    public void RemoveComment(Commentaries comment)
    {
        Delete(comment);
    }
}