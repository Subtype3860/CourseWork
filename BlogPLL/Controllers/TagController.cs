using AutoMapper;
using BlogDAL.Repository;
using BlogBLL.UnitOfWork;
using BlogBLL.ViewModels.Tag;
using BlogDAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogPLL.Controllers;

public class TagController : Controller
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<TagController> _logger;

    public TagController(IMapper mapper, IUnitOfWork unitOfWork, ILogger<TagController> logger)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult AddTag() => View();
    /// <summary>
    /// Создание Тег
    /// </summary>
    /// <param name="atvm">AddTagViewModel</param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult TagAdd(AddTagViewModel atvm)
    {
        if (!ModelState.IsValid) return RedirectToAction("AddTag");
        var model = _mapper.Map<Tag>(atvm);
        model.Id = Guid.NewGuid().ToString();
        var repository = _unitOfWork.GetRepository<Tag>() as TagRepository;
        repository!.AddTag(model);
        return RedirectToAction("Index", "Home");
    }
    /// <summary>
    /// Обновление TagToPost
    /// </summary>
    /// <param name="tagId"></param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult UpdateTag(string tagId )
    {
        if (string.IsNullOrEmpty(tagId)) return RedirectToAction("GoWrong", "Error");
        var repository = _unitOfWork.GetRepository<Tag>() as TagRepository;
        var model = repository!.GetTagById(tagId);
        var tag = _mapper.Map<EditTagViewModel>(model);
        return View(tag);
    }
    /// <summary>
    /// Обновление Тeг
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult TagUpdate(EditTagViewModel model)
    {
        if (!ModelState.IsValid) return RedirectToAction("UpdateTag");
        var repository = _unitOfWork.GetRepository<Tag>() as TagRepository;
        var tag = _mapper.Map<Tag>(model);
        repository!.UpdateTag(tag);
        return RedirectToAction("GetAllTag");
    }
    
    private IEnumerable<Tag> GetAll()
    {
        var repository = _unitOfWork.GetRepository<Tag>() as TagRepository;
        return repository!.GetAllTags();
    }
    /// <summary>
    /// Удаление связи PostToTag
    /// </summary>
    /// <param name="tagId"></param>
    /// <param name="postId"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult PostTagDelete(string tagId, string postId)
    {
        var repository = _unitOfWork.GetRepository<PostTag>() as PostTagRepository;
        var postTag = repository!.GetAllPostTags().FirstOrDefault(x => x.PostId == postId && x.TagId == tagId);
        if(postTag!=default)
            repository.DeletePostTag(postTag);
        return RedirectToAction("UpdatePost", "Post", new {id = postId});
    }

    public IActionResult RemoveTag(string id)
    {
        var repository = _unitOfWork.GetRepository<Tag>() as TagRepository;
        var tag = repository!.GetTagById(id);
        repository.TagRemove(tag);
        return RedirectToAction("GetAllTag", "Tag");
    }
    /// <summary>
    /// Получить все Теги
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetAllTag()
    {
        var ddd = User.Claims;
        if (!User.IsInRole("moderator")) return RedirectToAction("Forbidden", "Error");
        var model = GetAll();
        return View(model);
    }
    /// <summary>
    /// Получить Тег по ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetTeg(string id) 
    {
        var model = GetAll().FirstOrDefault(x=>x.Id == id);
        var teg = _mapper.Map<GetTagViewModel>(model);
        return View(teg);
    }
}