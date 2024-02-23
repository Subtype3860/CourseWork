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
    /// <param name="model">Передача модели Tag</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> TagAdd(AddTagViewModel atvm)
    {
        if (!ModelState.IsValid) return RedirectToAction("AddTag");
        var model = _mapper.Map<Tag>(atvm);
        var repository = _unitOfWork.GetRepository<Tag>() as TagRepository;
        await Task.Run(() => repository!.AddTag(model));
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult UpdateTag(EditTagViewModel model) => View(model);

    [HttpPost]
    public async Task<IActionResult> TagUpdate(EditTagViewModel etvm)
    {
        if (!ModelState.IsValid) return RedirectToAction("UpdateTag");
        var model = _mapper.Map<Tag>(etvm);
        var repository = _unitOfWork.GetRepository<Tag>() as TagRepository;
        await Task.Run(() => repository!.UpdateTag(model));
        return RedirectToAction("Index", "Home");
    }

    private async Task<IEnumerable<Tag>> GetAll()
    {
        var repository = _unitOfWork.GetRepository<Tag>() as TagRepository;
        return await Task.Run(() => repository!.GetAllTags());
    }

    [HttpPost]
    public async Task<IActionResult> TagDelete(string id)
    {
        var repository = _unitOfWork.GetRepository<Tag>() as TagRepository;
        var model = await Task.FromResult(repository!.GetAllTags());
        var tag = model.FirstOrDefault(x=>x.Id == id);
        await Task.Run(() => repository!.TagRemove(tag!));
        return RedirectToAction("Index", "Home");
    }
    [HttpGet]
    public IActionResult GetAllTag()
    {
        var model = (IEnumerable<GetTagViewModel>)GetAll().Result;
        return View(model);
    }
    [HttpGet]
    public IActionResult GetTeg(string id) 
    {
        var model = GetAll().Result.FirstOrDefault(x=>x.Id == id);
        var teg = _mapper.Map<GetTagViewModel>(model);
        return View(teg);
    }
}