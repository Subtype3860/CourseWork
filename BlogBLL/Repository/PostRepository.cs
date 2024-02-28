using AutoMapper;
using BlogDAL;
using BlogDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogBLL.Repository;

public class PostRepository : Repository<Post>
{
    private readonly IMapper _mapper;
    public PostRepository(ApplicationDbContext db, IMapper mapper) : base(db)
    {
        _mapper = mapper;
    }
    public IEnumerable<Post> GetAllPosts()
    {
        return Set
            .Include(x => x.PostTags)!
            .ThenInclude(x => x.Tag)
            .Include(x => x.User);
    }
    public Post GetPostById(string id)
    {
        return Set
            .Include(x => x.PostTags!)
            .ThenInclude(x => x.Tag)
            .Include(x => x.User).First(x => x.Id == id);
    }
    public IEnumerable<Post> GetPostByUserId(string id)
    {
        return Set
            .Where(x => x.UserId == id)
            .Include(x => x.PostTags!)
            .ThenInclude(x => x.Tag)
            .Include(x => x.User);
    }
    public void AddPost(Post post)
    {
        Create(post);
    }
    public void PostUpdate(Post post)
    {
        Update(post);
    }
    public void PostRemove(Post post)
    {
        Delete(post);
    }
    
}