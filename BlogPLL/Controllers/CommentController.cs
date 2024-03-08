using AutoMapper;
using BlogBLL.Repository;
using BlogBLL.UnitOfWork;
using BlogBLL.ViewModels.Comment;
using BlogDAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogPLL.Controllers;

public class CommentController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public CommentController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
    }
    [HttpGet]
    [Authorize]
    public IActionResult AddComment(AddCommentViewModel model) => View(model);
    /// <summary>
    /// Создание комментария
    /// </summary>
    /// <param name="acvm"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    public IActionResult CommentAdd(AddCommentViewModel acvm)
    {
        if (!ModelState.IsValid) return RedirectToAction();
        var user = User;
        var result = _userManager.GetUserAsync(user);
        acvm.Id = Guid.NewGuid().ToString();
        acvm.DatePublic = DateTime.Now;
        var model = _mapper.Map<Remark>(acvm);
        model.UserId = result.Result!.Id;
        var repository = _unitOfWork.GetRepository<Remark>() as RemarkRepository;
        repository!.AddComment(model);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [Authorize]
    
    public IActionResult UpdateComment(string id, string? comment)
    {
        var repository = _unitOfWork.GetRepository<Remark>() as RemarkRepository;
        var remark = repository!.GetRemarkById(id);
        remark.Comment = comment;
        var model = _mapper.Map<EditCommentViewModel>(remark);
        return View(model);
    }
    /// <summary>
    /// Редактирование коментария
    /// </summary>
    /// <param name="ecvm">Передача модели Comment</param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult CommentUpdate(EditCommentViewModel ecvm)
    {
        if (!ModelState.IsValid) return View("UpdateComment");
        var model = _mapper.Map<Remark>(ecvm);
        var repository = _unitOfWork.GetRepository<Remark>() as RemarkRepository;
        repository!.UpdateComment(model);
        return RedirectToAction("GetPostById", "Post", new{id = model.PostId});
    }
    /// <summary>
    /// Удаление комментария
    /// </summary>
    /// <param name="id">Передача Id Comment</param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    public IActionResult DeleteComment(string id)
    {
        var repository = _unitOfWork.GetRepository<Remark>() as RemarkRepository;
        var model = repository!.GetRemarkById(id);
        repository.RemoveComment(model);
        return RedirectToAction("Index", "Home");
    }
    [HttpGet]
    public IActionResult GetAllComment()
    {
        var model = GetAll();
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
        var model = GetAll().FirstOrDefault(x => x.Id == id);
        return View(model);
    }
    /// <summary>
    /// Получить все комментарии
    /// </summary>
    /// <returns></returns>
    private IEnumerable<GetCommentViewModel> GetAll()
    {
        var repository = _unitOfWork.GetRepository<Remark>() as RemarkRepository;
        var commentaries = repository!.GetAllComments();
        return (IEnumerable<GetCommentViewModel>)commentaries;
    }

}