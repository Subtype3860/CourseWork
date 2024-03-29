﻿using BlogDAL.Models;

namespace BlogBLL.ViewModels.Comment;

public class AddCommentViewModel
{
    /// <summary>
    /// ID комментария
    /// </summary>
    public string? Id { get; set; }
    /// <summary>
    /// Комментарий
    /// </summary>
    public string? Comment { get; set; }
    /// <summary>
    /// ID статьи
    /// </summary>
    public string? PostId { get; set; }
    /// <summary>
    /// Дата публикации
    /// </summary>
    public DateTime DatePublic { get; set; }
    /// <summary>
    /// ID пользователя
    /// </summary>
    public string? UserId { get; set; }
    /// <summary>
    /// Количество просмотров
    /// </summary>
    public byte Log { get; set; }
}