using BlogBLL.UnitOfWork;
using BlogDAL.Models;
using BlogDAL.Repository;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BlogBLL.Ext;
using BlogBLL.ViewModels.Post;

namespace BlogApi.Controllers;
/// <summary>
/// Controller PostApi реализует:
///     Получение всех статей;
///     Получение статьи по ID;
///     Добавление статьи;
///     Обновление статьи;
///     Удаление статьи.
/// </summary>
[ApiController]
[Route("[controller]")]
public class PostApiController : ControllerBase
{
    private readonly IUnitOfWork _db;
    private readonly IMapper _mapper;

    #region Designer

    /// <summary>
    /// Конструктор PostApiController
    /// </summary>
    /// <param name="db">IUnitOfWork</param>
    /// <param name="mapper">IMapper</param>
    public PostApiController(IUnitOfWork db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    #endregion

    #region GetAllPost HttpGet

    /// <summary>
    /// Получение всех статей
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("/Get")]
    public List<GetAllPostWebApi> Get()
    {
        var repository = _db.GetRepository<Post>() as PostRepository;
        var list = repository!.GetAllPosts();
        var enumerable = list as Post[] ?? list.ToArray();
        var model = new PostExtentions(null).GetAllPostExt(enumerable);
        return model;
    }
    

    #endregion

    #region GetPostById HttpGet

    /// <summary>
    /// Получение статьи по ID
    /// </summary>
    /// <param name="id">Type: string</param>
    /// <returns>model GetAllPostWebApi</returns>
    [HttpGet]
    [Route("/Get/{id}")]
    public GetAllPostWebApi Get(string id)
    {
        var repository = _db.GetRepository<Post>() as PostRepository;
        var post = repository!.GetPostById(id);
        var model = _mapper.Map<GetAllPostWebApi>(post);
        return model;
    }

    #endregion
    
    #region AddPost HttpPost

    /// <summary>
    /// Добавление статьи
    /// </summary>
    /// <param name="addPostWebApi">ViewModel</param>
    /// <returns></returns>
    [HttpPost]
    [Route("/AddPost/")]
    public IActionResult AddPost(AddPostWebApi addPostWebApi)
    {
        var repository = _db.GetRepository<Post>() as PostRepository;
        var model = _mapper.Map<Post>(addPostWebApi);
        repository!.AddPost(model);
        return Ok();
    }

    #endregion
    
    #region Update HttpPost

    /// <summary>
    /// Обновление статьи
    /// </summary>
    /// <param name="editPostWebApi">ViewModel</param>
    /// <returns></returns>
    [HttpPut]
    [Route("/UpdatePost/")]
    public IActionResult EditPost(EditPostWebApi editPostWebApi)
    {
        var repository = _db.GetRepository<Post>() as PostRepository;
        var model = _mapper.Map<Post>(editPostWebApi);
        repository!.PostUpdate(model);
        return Ok();
    }

    #endregion
    
    #region Delete HttpPost

    /// <summary>
    /// Удаление статьи
    /// </summary>
    /// <param name="id">string</param>
    /// <returns></returns>
    [HttpDelete]
    [Route("/DeletePost/{id}")]
    public IActionResult RemovePost(string id)
    {
        var repository = _db.GetRepository<Post>() as PostRepository;
        var model = repository!.GetPostById(id);
        repository.PostRemove(model);
        var repoPostTag = _db.GetRepository<PostTag>() as PostTagRepository;
        var postTag = repoPostTag!.GetPostTagByPostId(id);
        foreach (var tag in postTag)
        {
            repoPostTag.DeletePostTag(tag);
        }
        return Ok();
    }

    #endregion

    #region AddPostTeg

    /// <summary>
    /// Статья - Таг
    /// </summary>
    /// <param name="postId">ID Post</param>
    /// <param name="idTags">List Id Tags</param>
    /// <returns></returns>
    [HttpPost]
    [Route("/AddPostTeg")]
    public IActionResult AddPostTeg(string postId, List<string> idTags)
    {
        var repository = _db.GetRepository<PostTag>() as PostTagRepository;
        foreach (var tag in idTags)
        {
            repository!.AddPostTag(new PostTag { PostId = postId, TagId = tag });
        }
        return Ok();
    }

    #endregion
    

}