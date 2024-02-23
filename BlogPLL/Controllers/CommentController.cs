using AutoMapper;
using BlogBLL.Repository;
using BlogBLL.UnitOfWork;
using BlogBLL.ViewModels.Comment;
using BlogDAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogPLL.Controllers;

public class CommentController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CommentController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    public IActionResult AddComment(AddCommentViewModel model) => View(model);
    /// <summary>
    /// Создание комментария
    /// </summary>
    /// <param name="acvm"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CommentAdd(AddCommentViewModel acvm)
    {
        if (!ModelState.IsValid) return RedirectToAction();
        var model = _mapper.Map<Commentaries>(acvm);
        var repository = _unitOfWork.GetRepository<Commentaries>() as CommentRepository;
        await Task.Run(() => repository!.AddComment(model));
        return RedirectToAction("Index", "Home");
    }
    [HttpGet]
    public IActionResult UpdateComment(EditCommentViewModel model) => View(model);
    /// <summary>
    /// Редактирование коментария
    /// </summary>
    /// <param name="ecvm">Передача модели Comment</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CommentUpdate(EditCommentViewModel ecvm)
    {
        if (!ModelState.IsValid) return View("UpdateComment");
        var model = _mapper.Map<Commentaries>(ecvm);
        var repository = _unitOfWork.GetRepository<Commentaries>() as CommentRepository;
        await Task.Run(() => repository!.UpdateComment(model));
        return RedirectToAction("Index", "Home");
    }
    /// <summary>
    /// Удаление комментария
    /// </summary>
    /// <param name="id">Передача Id Comment</param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult DeleteComment(string id)
    {
        var repository = _unitOfWork.GetRepository<Commentaries>() as CommentRepository;
        var model = GetAll().Result.FirstOrDefault(x => x.Id == id);
        repository!.RemoveComment(model!);
        return RedirectToAction("Index", "Home");
    }
    [HttpGet]
    public IActionResult GetAllComment()
    {
        var model = GetAll().Result;
        return View(model);
    }
    /// <summary>
    /// Получить комментарий по ID
    /// </summary>
    /// <param name="id">Передача ID Comment</param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetComment(string id)
    {
        var model = GetAll().Result.FirstOrDefault(x => x.Id == id);
        return View(model);
    }
    /// <summary>
    /// Получить все комментарии
    /// </summary>
    /// <returns></returns>
    private async Task<IEnumerable<GetCommentViewModel>> GetAll()
    {
        var repository = _unitOfWork.GetRepository<Commentaries>() as CommentRepository;
        var commentaries = await Task.FromResult(repository!.GetAllComments());
        return (IEnumerable<GetCommentViewModel>)commentaries;
    }

}