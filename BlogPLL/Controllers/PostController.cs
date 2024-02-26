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
    public async Task<IActionResult> AddPost(AddPostViewModel apvm)
    {
        //Создание блога
        var postRepository = _unitOfWork.GetRepository<Post>() as PostRepository;
        var post = _mapper.Map<Post>(apvm);
        await Task.Run(() => postRepository!.AddPost(post));

        //Присвоени тегов
        var tagRepository = _unitOfWork.GetRepository<Tag>() as TagRepository;
        var postTagRepository = _unitOfWork.GetRepository<PostTag>() as PostTagRepository;

        var listTag = apvm.Tag!.Split(" ");
        foreach (var s in listTag)
        {
            var w = tagRepository!.GetAllTags().FirstOrDefault(x => x.Stick!.ToLower() == s);
            
            var y = new Tag
            {
                Id = w == null? Guid.NewGuid().ToString():w.Id,
                Stick = w == null? s :w.Stick,
            };
            if(w == null)
                await Task.Run(() => tagRepository.AddTag(y));

            await Task.Run(() => postTagRepository!.AddPostTag(new PostTag{PostId = post.Id, TagId = y.Id}));
        }
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult UpdatePost(string id)
    {
        if (string.IsNullOrEmpty(id)) return RedirectToAction("Index", "Home");
        var repository = _unitOfWork.GetRepository<Post>() as PostRepository;
        var post = repository!.GetPostById(id);
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

        var x = new Post
        {
            Id = "333bd4d6-edf3-4863-9148-c26987fd2ac8",
            User = epvm.User,
            PostTags = epvm.PostTags,
            UserId = epvm.UserId,
            Body = epvm.Post,
            Heading = epvm.Heading,
            DatePublic = epvm.DatePublic
        };
        
        
        
        
        var repositoryPost = _unitOfWork.GetRepository<Post>() as PostRepository;
        
        // var repositoryPostTag = _unitOfWork.GetRepository<PostTag>() as PostTagRepository;
        // var repositoryTeg = _unitOfWork.GetRepository<Tag>() as TagRepository;
        //
        // //редактирование тегов
        // var post = await Task.FromResult(repositoryPost!.GetPostById(epvm.PostId!));
        // var listTag = epvm.Tag!.Split("#");
        // foreach (var postTag in post.PostTags!.Where(x=>x.PostId==epvm.PostId))
        // {
        //     var x = listTag.FirstOrDefault(w => string.Equals(w, postTag.Tag!.Stick));
        //     if (string.IsNullOrEmpty(x))
        //     {
        //         repositoryPostTag!.DeletePostTag(postTag);
        //     }
        // }
        // foreach (var s in listTag)
        // {
        //     if(string.IsNullOrEmpty(s))continue;
        //     var postTag = post.PostTags!
        //         .Where(x => x.PostId == epvm.PostId)
        //         .FirstOrDefault(x => string.Equals(x.Tag!.Stick, s));
        //     if (postTag == default)
        //     {
        //         var tagId = Guid.NewGuid().ToString();
        //         repositoryTeg!.AddTag(new Tag{Id = tagId, Stick = s});
        //         repositoryPostTag!.AddPostTag(new PostTag{PostId = epvm.PostId, TagId = tagId});
        //     }
        // }
        
        
        
        var model = _mapper.Map<Post>(epvm);
        
        await Task.Run(() => repositoryPost!.PostUpdate(x));
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
    /// <summary>
    /// Получение всех статей
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetAllPost()
    {
        var repository = _unitOfWork.GetRepository<Post>() as PostRepository;
        var model = repository!.GetAll();
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
        if (user == null) return RedirectToAction("GetAllPost");
        var repository = _unitOfWork.GetRepository<Post>() as PostRepository;
        var post = await Task.FromResult(repository!.GetPostByUserId(id));
        return View(post);
    }

    [HttpGet]
    public async Task<IActionResult> GetPostById(string id)
    {
        var user = User;
        var userId = _userManager.GetUserAsync(user);
        var repository = _unitOfWork.GetRepository<Post>() as PostRepository;
        var post = await Task.FromResult(repository!.GetPostById(id));
        var model = _mapper.Map<GetPostViewModel>(post);
        if(user.Identity!.IsAuthenticated)
            model.Log = string.Equals(userId.Result!.Id, model.UserId);
        return View(model);
    }
}