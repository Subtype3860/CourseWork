using AutoMapper;
using BlogBLL.Repository;
using BlogBLL.UnitOfWork;
using BlogBLL.ViewModels.Post;
using BlogDAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogPLL.Controllers;

public class PostController : Controller
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public PostController(IMapper mapper, IUnitOfWork unitOfWork, UserManager<User> userManager)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }
    [HttpGet]
    public IActionResult AddPost() => View();
    /// <summary>
    /// Создание статьи
    /// </summary>
    /// <param name="post">Передача string "Статья"</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> AddPost(string post)
    {
        var user = User;
        //Создание блога
        var result = await _userManager.GetUserAsync(user);
        var repository = _unitOfWork.GetRepository<Post>() as PostRepository;
        await Task.Run(() => repository!.AddPost(post, result!));
        return RedirectToAction("Index", "Home");
    }
    [HttpGet]
    public IActionResult UpdatePost(EditPostViewModel model) => View(model);
    /// <summary>
    /// Обновление статьи
    /// </summary>
    /// <param name="epvm">Передача ViewModel "EditPostViewModel"</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> PostUpdate(EditPostViewModel epvm)
    {
        if (!ModelState.IsValid) return RedirectToAction("Index", "Home");
        var model = _mapper.Map<Post>(epvm);
        var repository = _unitOfWork.GetRepository<Post>() as PostRepository;
        await Task.Run(() => repository!.PostUpdate(model));
        return RedirectToAction("Index", "Home");
    }
    /// <summary>
    /// Удаление статьи
    /// </summary>
    /// <param name="id">Передача Id Post</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> DeletePost(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            //Удаление связей с Тегами
            var repositoryPostTeg = _unitOfWork.GetRepository<PostTag>() as PostTagRepository;
            var postTag = 
                await Task.FromResult(repositoryPostTeg!.GetAllPostTags().Where(x => x.PostId == id));
            foreach (var pt in postTag)
            {
                await Task.Run(() => repositoryPostTeg.Delete(pt));
            }
            //Удаление статьи
            var repositoryPost = _unitOfWork.GetRepository<Post>() as PostRepository;
            var post = await Task.FromResult(repositoryPost!.GetAllPosts().FirstOrDefault(x => x.Id == id));
            await Task.Run(() => repositoryPost.Delete(post!));
        }

        return RedirectToAction("Index", "Home");
    }

    private async Task<IEnumerable<Post>> GetAll()
    {
        var repository = _unitOfWork.GetRepository<Post>() as PostRepository;
        var post = await Task.FromResult(repository!.GetAllPosts());
        return post;
    }
    /// <summary>
    /// Получение всех статей
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetAllPost()
    {
        var post = GetAll().Result;
        var model = new GetPostViewModel
        {
            Posts = post
        };
        return View(model);
    }
    /// <summary>
    /// Получение статьи пользователя
    /// </summary>
    /// <param name="id">Передача Id User</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult>GetPostByUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return RedirectToAction("GetAllPost");
        var post = GetAll().Result.Where(x => x.UserId == user.Id);
        var model = new GetPostViewModel
        {
            Posts = post
        };
        return View(model);
    }
}