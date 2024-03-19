using BlogBLL.UnitOfWork;
using BlogBLL.ViewModels.Comment;
using BlogDAL.Models;
using BlogDAL.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentApiController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommentApiController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Получить список комментариев
        /// </summary>
        /// <returns>IEnumerable<Remark></Remark></returns>
        [HttpGet]
        [Route("GetRemarks")]
        public IEnumerable<Remark> GetRemarks()
        {
            var repository = _unitOfWork.GetRepository<Remark>() as RemarkRepository;
            var model = repository!.GetAllComments();
            return model;
        }

    }
}
