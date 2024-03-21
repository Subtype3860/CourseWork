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

}