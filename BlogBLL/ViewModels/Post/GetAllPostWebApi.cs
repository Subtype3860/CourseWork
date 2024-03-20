namespace BlogBLL.ViewModels.Post;

public class GetAllPostWebApi
{
    /// <summary>
    /// ID Статьи
    /// </summary>
    public string? Id { get; set; }
    /// <summary>
    /// Статья
    /// </summary>
    public string Body { get; set; } = string.Empty;

    /// <summary>
    /// ID пользователя
    /// </summary>
    public string UserId { get; set; } = string.Empty;
    /// <summary>
    /// Дата публикации
    /// </summary>
    public DateTime DatePublic { get; set; } = DateTime.Now;

    /// <summary>
    /// Заголовок статьи
    /// </summary>
    public string? Heading { get; set; } = string.Empty;
}