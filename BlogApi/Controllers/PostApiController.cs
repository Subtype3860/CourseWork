using BlogBLL.UnitOfWork;
using BlogDAL.Models;
using BlogDAL.Repository;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BlogBLL.ViewModels.Post;

namespace BlogApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PostApiController : ControllerBase
{
    private readonly IUnitOfWork _db;
    private readonly IMapper _mapper;

    public PostApiController(IUnitOfWork db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    /// <summary>
    /// Получение всех статей
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("/Get")]
    public IEnumerable<Post> Get()
    {
        var repository = _db.GetRepository<Post>() as PostRepository;
        var list = repository!.GetAllPosts();
        return list;
    }
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
        return Ok();
    }
}