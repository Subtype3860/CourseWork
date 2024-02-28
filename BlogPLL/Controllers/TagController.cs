using AutoMapper;
using BlogBLL.Repository;
using BlogBLL.UnitOfWork;
using BlogBLL.ViewModels.Tag;
using BlogDAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogPLL.Controllers;

public class TagController : Controller
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public TagController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public IActionResult AddTag(AddTagViewModel model) => View(model);
    /// <summary>
    /// Создание тэг
    /// </summary>
    /// <param name="atvm">AddTagViewModel</param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult TagAdd(AddTagViewModel atvm)
    {
        if (!ModelState.IsValid) return RedirectToAction("AddTag");
        var model = _mapper.Map<Tag>(atvm);
        var repository = _unitOfWork.GetRepository<Tag>() as TagRepository;
        repository!.AddTag(model);
        return RedirectToAction("Index", "Home");
    }
    /// <summary>
    /// Обновление TagToPost
    /// </summary>
    /// <param name="tags"></param>
    /// <param name="postId"></param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult UpdateTag(string? tags, string postId)
    {
        if (string.IsNullOrEmpty(tags)) return NotFound();
        var stringTags = tags.Split("#");
        var listTag = new List<Tag>();
        foreach (var tag in stringTags)
        {
            if(string.IsNullOrEmpty(tag)) continue;
            var repository = _unitOfWork.GetRepository<Tag>() as TagRepository;
            var tagByName = repository!.GetTagByName(tag);
            listTag.Add(tagByName);
        }

        var model = new EditTagViewModel
        {
            Tags = listTag,
            PostId = postId
        };
        return View(model);
    }
    /// <summary>
    /// Обновление Тэг
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="tagIp"></param>
    /// <param name="postId"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult TagUpdate(string tag, string tagId, string postId)
    {
        if (!ModelState.IsValid) return RedirectToAction("UpdateTag");
        var repository = _unitOfWork.GetRepository<Tag>() as TagRepository;
        repository!.UpdateTag(new Tag{Id = tagId, Stick = tag});
        return RedirectToAction("UpdatePost", "Post", new {id = postId});
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
        repository!.DeletePostTag(postTag!);
        return RedirectToAction("UpdatePost", "Post", new {id = postId});
    }
    /// <summary>
    /// Получить все тэги
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetAllTag()
    {
        var model = GetAll();
        return View((IEnumerable<GetTagViewModel>)model);
    }
    /// <summary>
    /// Получить тэг по ID
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