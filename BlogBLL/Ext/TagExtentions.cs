using BlogBLL.UnitOfWork;
using BlogBLL.ViewModels.Tag;
using BlogDAL.Models;

namespace BlogBLL.Ext;

public class TagExtentions
{
    #region GetAllTag

        /// <summary>
        /// Получаем лист Тег
        /// </summary>
        /// <param name="model">IEnumerable Tag</param>
        /// <returns>List GetAllTagWebApi </returns>
    
        public List<GetAllTagWebApi> GetTagToViewModel(IEnumerable<Tag> model)
        {
            var getAllTagWebApi = new List<GetAllTagWebApi>();
            foreach (var tag in model)
            {
                getAllTagWebApi.Add(new GetAllTagWebApi
                {
                    Id = tag.Id,
                    Tag = tag.Stick
                });
            }
    
            return getAllTagWebApi;
        }

    #endregion
}
