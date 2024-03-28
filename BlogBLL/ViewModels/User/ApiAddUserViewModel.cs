namespace BlogBLL.ViewModels.User;

public class ApiAddUserViewModel
{
    /// <summary>
    /// ID пользователя
    /// </summary>
    public string Id { get; set; } = string.Empty;
    /// <summary>
    /// О пользователе
    /// </summary>
    public string About { get; set; } = string.Empty;
    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime? BirthDate { get; set; }
    /// <summary>
    /// Электронный адрес
    /// </summary>
    public string Email { get; set; } = string.Empty;
    /// <summary>
    /// Имя
    /// </summary>
    public string FirstName { get; set; } = string.Empty;
    /// <summary>
    /// Фамилия
    /// </summary>
    public string LastName { get; set; } = string.Empty;
    /// <summary>
    /// Отчество
    /// </summary>
    public string MiddleName { get; set; } = string.Empty;
    /// <summary>
    /// Логин
    /// </summary>
    public string UserName { get; set; } = string.Empty;
    /// <summary>
    /// Номер телефона
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;
    /// <summary>
    /// Статус
    /// </summary>
    public string Status { get; set; } = string.Empty;
}