namespace BlogBLL.ViewModels.Role;

public class ApiAddRoleViewModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string About { get; set; } = string.Empty;
}