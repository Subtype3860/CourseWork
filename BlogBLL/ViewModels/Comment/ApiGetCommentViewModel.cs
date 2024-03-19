using Microsoft.AspNetCore.Identity;
using static System.Net.Mime.MediaTypeNames;

namespace BlogBLL.ViewModels.Comment
{
    public class ApiGetCommentViewModel
    {
        /// <summary>
        /// ID комментария
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment { get; set; } = string.Empty;
        /// <summary>
        /// ID статьи
        /// </summary>
        public string PostId { get; set; } = string.Empty;
        /// <summary>
        /// ID пользователя
        /// </summary>
        public string? UserId { get; set; }
        /// <summary>
        /// Дата публикации
        /// </summary>
        public DateTime DatePublic { get; set; } = DateTime.Now;
    }
}
