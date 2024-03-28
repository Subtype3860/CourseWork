using AutoMapper;
using BlogBLL.Ext;
using BlogBLL.UnitOfWork;
using BlogBLL.ViewModels.Comment;
using BlogDAL.Models;
using BlogDAL.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers;

/// <summary>
/// Класс для работы с комментариями
/// </summary>
[ApiController]
[Route("[controller]")]
public class RemarkApiController : ControllerBase
{
    #region Designer

    private readonly IUnitOfWork _db;
    private readonly IMapper _mapper;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="db">IUnitOfWork</param>
    /// <param name="mapper">IMapper</param>
    public RemarkApiController(IUnitOfWork db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    #endregion

    #region GetAllComment

    /// <summary>
    /// Получить все комментарии
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("/GetAllComment")]
    public List<ApiGetCommentViewModel> GetAllComment()
    {
        var repository = _db.GetRepository<Remark>() as RemarkRepository;
        var remarks = repository!.GetAllComments();
        var model = new RemarkExtentions().GetRemarkToViewModel(remarks);
        return model;
    }

    #endregion

    #region GetCommetById

    /// <summary>
    /// Получение комментария по ID
    /// </summary>
    /// <param name="id">ID комментария</param>
    /// <returns></returns>
    [HttpGet]
    [Route("/GetCommetById/{id}")]
    public ApiGetRemarkByIdViewModel GetCommetById(string id)
    {
        var repository = _db.GetRepository<Remark>() as RemarkRepository;
        var remark = repository!.GetRemarkById(id);
        var model = _mapper.Map<ApiGetRemarkByIdViewModel>(remark);
        return model;
    }

    #endregion

    #region AddRemark

    /// <summary>
    /// Добавление коментария 
    /// </summary>
    /// <param name="model">Модель Remark</param>
    /// <returns></returns>
    [HttpPost]
    [Route("/AddRemark")]
    public IActionResult AddRemark(ApiAddRemarkViewModel model)
    {
        if (!ModelState.IsValid) return NotFound();
        var remark = _mapper.Map<Remark>(model);
        var repository = _db.GetRepository<Remark>() as RemarkRepository;
        repository!.AddComment(remark);
        return Ok();
    }

    #endregion

    #region UpdateRemark

    /// <summary>
    /// Обновление комментария
    /// </summary>
    /// <param name="model">Модель Remark</param>
    /// <returns></returns>
    [HttpPut]
    [Route("/UpdareRemark")]
    public IActionResult UpdateRemark(ApiUpdateRemarkViewModel model)
    {
        if (!ModelState.IsValid) return NotFound();
        var remark = _mapper.Map<Remark>(model);
        var repository = _db.GetRepository<Remark>() as RemarkRepository;
        repository!.UpdateComment(remark);
        return Ok();
    }

    #endregion

    #region RemoveRemark

    /// <summary>
    /// Удаление комментария
    /// </summary>
    /// <param name="id">ID комментария</param>
    /// <returns></returns>
    [HttpDelete]
    [Route("/RemoveRemark")]
    public IActionResult RemoveRemark(string id)
    {
        var repository = _db.GetRepository<Remark>() as RemarkRepository;
        repository!.RemoveComment(id);
        
        return Ok();
    }

    #endregion

}