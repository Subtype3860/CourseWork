using AutoMapper;
using BlogBLL.Ext;
using BlogBLL.UnitOfWork;
using BlogBLL.ViewModels.Tag;
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
        #region Designer

        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор TagApiController
        /// </summary>
        /// <param name="db">IUnitOfWork</param>
        /// <param name="mapper">IMapper</param>
        public TagApiController(IUnitOfWork db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        #endregion

        #region GetAllTag HttpGet

        /// <summary>
        /// Получение всех тегов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/GetAllTag")]
        public List<GetAllTagWebApi>? GetAllTag()
        {
            if (ModelState.IsValid)
            {
                var repository = _db.GetRepository<Tag>() as TagRepository;
                var tags = repository!.GetAllTags();
                var enumerable = tags as Tag[] ?? tags.ToArray();
                var model = new TagExtentions().GetTagToViewModel(enumerable);
                return model;
            }

            return null;
        }

        #endregion

        #region GetTagById HttpGet

        /// <summary>
        /// Получение Тега по ID
        /// </summary>
        /// <param name="id">ID тега</param>
        /// <returns>Модель Tag</returns>
        [HttpGet]
        [Route("/GetTagById")]
        public GetTagByIdWebApi? GetTagById(string id)
        {
            if (ModelState.IsValid)
            {
                var repository = _db.GetRepository<Tag>() as TagRepository;
                var tag = repository!.GetTagById(id);
                var model = _mapper.Map<GetTagByIdWebApi>(tag);
                return model;
            }

            return null;
        }

        #endregion

        #region GetTagByPostName

        /// <summary>
        /// Получить теги по названию статьи
        /// </summary>
        /// <param name="id">ID статьи</param>
        /// <returns>Список тегов</returns>
        [HttpGet]
        [Route("/GetTagByPostName")]
        public List<GetAllTagWebApi> GetTagByPostName(string id)
        {
            var repository = _db.GetRepository<Tag>() as TagRepository;
            var tags = repository!.GetAllTags();
            var postTags = tags.Select(s=>s.PostTags.First(f=>f.Post!.Id == id)).Select(s=>s.Tag);
            var model = new TagExtentions().GetTagToViewModel(postTags!);
            return model;
        }

        #endregion

        #region AddTag HttpPost

        /// <summary>
        /// Добавить тег
        /// </summary>
        /// <param name="model">AddTagWebApi</param>
        /// <returns></returns>
        [HttpPost]
        [Route("/AddTag")]
        public IActionResult AddTag(AddTagWebApi model)
        {
            if (ModelState.IsValid)
            {
                var tag = _mapper.Map<Tag>(model);
                var repository = _db.GetRepository<Tag>() as TagRepository;
                repository!.AddTag(tag);
                return Ok();
            }

            return NotFound();
        }

        #endregion

        #region UpdateTag HttpPut

        /// <summary>
        /// Редактирование тега
        /// </summary>
        /// <param name="model">EditTagWebApi</param>
        /// <returns></returns>
        [HttpPut]
        [Route("/UpdateTag")]
        public IActionResult UpdateTag(EditTagWebApi model)
        {
            if (ModelState.IsValid)
            {
                var tag = _mapper.Map<Tag>(model);
                var repository = _db.GetRepository<Tag>() as TagRepository;
                repository!.UpdateTag(tag);
                return Ok();
            }

            return NotFound();
        }

        #endregion

        #region DeleteTag HttpDelete

        /// <summary>
        /// Удаление тега
        /// </summary>
        /// <param name="id">ID Тега</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/DeleteTag")]
        public IActionResult DeleteTag(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var repository = _db.GetRepository<Tag>() as TagRepository;
            var model = repository!.GetTagById(id);
            repository!.TagRemove(model);
            return Ok();
        }
        #endregion

    }
}
