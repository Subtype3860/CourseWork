namespace BlogDAL.Models;

public class Remark
{
    /// <summary>
    /// ID комментария
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Комментария
    /// </summary>
    public string Comment { get; set; } = string.Empty;

    /// <summary>
    /// ID статьи
    /// </summary>
    public string PostId { get; set; } = string.Empty;
    public Post? Post { get; set; }

    /// <summary>
    /// ID пользователя
    /// </summary>
    public string UserId { get; set; } = string.Empty;
    public User? User { get; set; }
    /// <summary>
    /// Дата публикации
    /// </summary>
    public DateTime DatePublic { get; set; } = DateTime.Now;

}