namespace BlogDAL.Models;

public class Remark
{
    /// <summary>
    /// ID комментария
    /// </summary>
    public string? Id { get; set; }
    /// <summary>
    /// Комментария
    /// </summary>
    public string? Comment { get; set; }
    /// <summary>
    /// ID статьи
    /// </summary>
    public string? PostId { get; set; }
    public Post? Post { get; set; }
    /// <summary>
    /// ID пользователя
    /// </summary>
    public string? UserId { get; set; }
    public User? User { get; set; }
    /// <summary>
    /// Дата публикации
    /// </summary>
    public DateTime DatePublic { get; set; }

}