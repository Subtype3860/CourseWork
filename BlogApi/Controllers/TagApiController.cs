using BlogBLL.UnitOfWork;
using BlogDAL.Models;
using BlogDAL.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    /// <summary>
    /// Класс Тег
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TagApiController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        public TagApiController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Получение всех тегов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/GetAllTag")]
        public List<Tag> GetAllTag()
        {
            var repository = _unitOfWork.GetRepository<Tag>() as TagRepository;
            var model = repository!.GetAllTags();
            return model.ToList();
        }
    }
}
