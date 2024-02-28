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
    public async Task<IActionResult> AddPost()
    {
        var user = User;
        var result = await _userManager.GetUserAsync(user);
        var model = new AddPostViewModel
        {
            PostId = Guid.NewGuid().ToString(),
            UserId = result!.Id
        };
        return View(model);
    }

    /// <summary>
    /// Создание статьи
    /// </summary>
    /// <param name="apvm">AddPostViewModel</param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult AddPost(AddPostViewModel apvm)
    {
        //Создание блога
        var postRepository = _unitOfWork.GetRepository<Post>() as PostRepository;
        var post = _mapper.Map<Post>(apvm);
        postRepository!.AddPost(post);

        //Присвоение тегов
        var tagRepository = _unitOfWork.GetRepository<Tag>() as TagRepository;
        var postTagRepository = _unitOfWork.GetRepository<PostTag>() as PostTagRepository;

        if (!string.IsNullOrEmpty(apvm.Tag))
        {
            var listChar = new[] { '_', ',', '.', '#', ';' };

            var listTag = apvm.Tag!.Replace("#", " ").Replace(";", " ").Replace("_", " ").Split(" ");
            foreach (var s in listTag)
            {
                var w = tagRepository!.GetAllTags().FirstOrDefault(x => x.Stick!.ToLower() == s);

                var y = new Tag
                {
                    Id = w == null ? Guid.NewGuid().ToString() : w.Id,
                    Stick = w == null ? s : w.Stick,
                };
                if (w == null)
                    tagRepository.AddTag(y);

                postTagRepository!.AddPostTag(new PostTag { PostId = post.Id, TagId = y.Id });
            }
        }
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult UpdatePost(string id)
    {
        if (string.IsNullOrEmpty(id)) return RedirectToAction("Index", "Home");
        var repository = _unitOfWork.GetRepository<Post>() as PostRepository;
        var post = repository!.GetAllPosts().FirstOrDefault(x=>x.Id == id);
        var model = _mapper.Map<EditPostViewModel>(post);
        return View(model);
    }
    /// <summary>
    /// Обновление статьи
    /// </summary>
    /// <param name="epvm">Передача ViewModel "EditPostViewModel"</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> PostUpdate(EditPostViewModel epvm)
    {
        if (!ModelState.IsValid) return RedirectToAction("Index", "Home");
        var repositoryPost = _unitOfWork.GetRepository<Post>() as PostRepository;
        var model = _mapper.Map<Post>(epvm);
        await Task.Run(() => repositoryPost!.PostUpdate(model));
        return RedirectToAction("Index", "Home");
    }
    /// <summary>
    /// Удаление статьи
    /// </summary>
    /// <param name="id">Передача Id Post</param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult DeletePost(string id)
    {
        if (!string.IsNullOrEmpty(id))
        {
            //Удаление статьи
            var repositoryPost = _unitOfWork.GetRepository<Post>() as PostRepository;
            var post = repositoryPost!.GetAllPosts().FirstOrDefault(x => x.Id == id);
            repositoryPost.Delete(post!);

            //Удаление связей с Тегами
            var repositoryPostTeg = _unitOfWork.GetRepository<PostTag>() as PostTagRepository;
            var postTag =
                repositoryPostTeg!.GetAllPostTags().Where(x => x.PostId == id);
            foreach (var pt in postTag)
            {
                repositoryPostTeg.Delete(pt);
            }
        }
        return RedirectToAction("Index", "Home");
    }
    /// <summary>
    /// Получение всех статей
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetAll()
    {
        var repository = _unitOfWork.GetRepository<Post>() as PostRepository;
        var model = repository!.GetAllPosts();
        return View(model);
    }
    /// <summary>
    /// Получение статьи пользователя
    /// </summary>
    /// <param name="id">Передача Id User</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult>GetPostByUserId(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return RedirectToAction("GetAll");
        var repository = _unitOfWork.GetRepository<Post>() as PostRepository;
        var post = repository!.GetPostByUserId(id);
        return View(post);
    }

    [HttpGet]
    public async Task<IActionResult> GetPostById(string id)
    {
        var user = User;
        var userId = await _userManager.GetUserAsync(user);
        var repository = _unitOfWork.GetRepository<Post>() as PostRepository;
        var post = repository!.GetPostById(id);
        var model = _mapper.Map<GetPostViewModel>(post);
        var repositoryRemark = _unitOfWork.GetRepository<Remark>() as RemarkRepository;
        var modelRemarks = repositoryRemark!.GetCommentByPostId(model.Id!);
        model.Remarks = modelRemarks.ToList();
        model.LoUserId = userId!.Id;
        if(user.Identity!.IsAuthenticated)
            model.Log = string.Equals(userId!.Id, model.UserId);
        return View(model);
    }
}