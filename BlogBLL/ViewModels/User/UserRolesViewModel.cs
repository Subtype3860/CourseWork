using BlogDAL.Models;

namespace BlogBLL.ViewModels.User;

public class UserRolesViewModel
{
    public BlogDAL.Models.User? User { get; set; }
    public virtual IList<string>? Roles { get; set; }
}